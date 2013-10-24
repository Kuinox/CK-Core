﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CK.Core
{
    /// <summary>
    /// Simple collector of log entries which level is greater or equal to <see cref="MinimalFilter"/>.
    /// Its <see cref="Capacity"/> defaults to 50 (no more than Capacity entries are kept).
    /// </summary>
    public class ActivityMonitorSimpleCollector : IActivityMonitorClient
    {
        readonly FIFOBuffer<Entry> _entries;
        LogLevelFilter _filter;

        /// <summary>
        /// Element of the <see cref="ActivityMonitorSimpleCollector.Entries">Entries</see>.
        /// </summary>
        public class Entry
        {
            /// <summary>
            /// The tags of the log entry.
            /// </summary>
            public readonly CKTrait Tags;

            /// <summary>
            /// The log level of the log entry.
            /// </summary>
            public readonly LogLevel MaskedLevel;

            /// <summary>
            /// Timestamp of the log entry.
            /// </summary>
            public readonly DateTime LogTimeUtc;

            /// <summary>
            /// The text of the log entry.
            /// </summary>
            public readonly string Text;

            /// <summary>
            /// The exception of the log entry if any.
            /// </summary>
            public readonly Exception Exception;

            internal Entry( CKTrait tags, LogLevel level, string text, DateTime logTimeUtc, Exception ex )
            {
                Debug.Assert( (level & LogLevel.IsFiltered) == 0 );
                Tags = tags;
                MaskedLevel = level;
                LogTimeUtc = logTimeUtc;
                Text = text;
                Exception = ex;
            }
            
            /// <summary>
            /// Overriden to return the <see cref="Text"/> of this element.
            /// </summary>
            /// <returns>This <see cref="Text"/> property.</returns>
            public override string ToString()
            {
                return Text;
            }
        }

        /// <summary>
        /// Initializes a new collector with an initial capacity of 50 errors (<see cref="MinimalFilter"/> is set to <see cref="LogLevelFilter.Error"/>).
        /// </summary>
        public ActivityMonitorSimpleCollector()
        {
            _entries = new FIFOBuffer<Entry>( 50 );
            _filter = LogLevelFilter.Error;
        }

        /// <summary>
        /// Gets or sets the maximum numbers of <see cref="Entry"/> that must be kept in <see cref="Entries"/>.
        /// Defaults to 50.
        /// </summary>
        public int Capacity
        {
            get { return _entries.Capacity; }
            set { _entries.Capacity = value; }
        }

        /// <summary>
        /// Gets or sets the filter level.
        /// </summary>
        public LogLevelFilter MinimalFilter
        {
            get { return _filter; }
            set 
            {
                if( value > _filter )
                {
                    if( value != LogLevelFilter.Off )
                    {
                        Entry[] exist = _entries.ToArray();
                        _entries.Clear();
                        foreach( var e in exist )
                            if( (int)e.MaskedLevel >= (int)value ) _entries.Push( e );
                    }
                    else _entries.Clear();
                }
                _filter = value; 
            }
        }

        /// <summary>
        /// Gets a read only list of (at most) <see cref="Capacity"/> entries that occured since last 
        /// call to <see cref="Clear"/>.
        /// </summary>
        public IReadOnlyList<Entry> Entries
        {
            get { return _entries; }
        }

        /// <summary>
        /// Clears the current <see cref="Entries"/> list.
        /// </summary>
        public void Clear()
        {
            _entries.Clear();
        }

        /// <summary>
        /// Appends any log with level equal or above <see cref="MinimalFilter"/> to <see cref="Entries"/>.
        /// </summary>
        /// <param name="data">Log data. Never null.</param>
        void IActivityMonitorClient.OnUnfilteredLog( ActivityMonitorData data )
        {
            var level = data.Level & LogLevel.Mask;
            if( (int)level >= (int)_filter )
            {
                _entries.Push( new Entry( data.Tags, level, data.Text, data.LogTimeUtc, data.Exception ) );
            }
        }

        /// <summary>
        /// Appends any group with level equal or above <see cref="MinimalFilter"/> to <see cref="Entries"/>.
        /// </summary>
        /// <param name="group">Log group description.</param>
        void IActivityMonitorClient.OnOpenGroup( IActivityLogGroup group )
        {
            if( (int)group.MaskedGroupLevel >= (int)_filter )
            {
                _entries.Push( new Entry( group.GroupTags, group.MaskedGroupLevel, group.GroupText, group.LogTimeUtc, group.Exception ) );
            }
        }

        void IActivityMonitorClient.OnGroupClosing( IActivityLogGroup group, ref List<ActivityLogGroupConclusion> conclusions )
        {
        }

        void IActivityMonitorClient.OnGroupClosed( IActivityLogGroup group, IReadOnlyList<ActivityLogGroupConclusion> conclusions )
        {
        }


        void IActivityMonitorClient.OnTopicChanged( string newTopic, string fileName, int lineNumber )
        {
        }

        void IActivityMonitorClient.OnAutoTagsChanged( CKTrait newTrait )
        {
        }
    }
}