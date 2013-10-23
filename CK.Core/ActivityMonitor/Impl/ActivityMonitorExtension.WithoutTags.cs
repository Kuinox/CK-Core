#region LGPL License
/*----------------------------------------------------------------------------
* This file (CK.Core\ActivityMonitor\Impl\ActivityMonitorExtension.cs) is part of CiviKey. 
*  
* CiviKey is free software: you can redistribute it and/or modify 
* it under the terms of the GNU Lesser General Public License as published 
* by the Free Software Foundation, either version 3 of the License, or 
* (at your option) any later version. 
*  
* CiviKey is distributed in the hope that it will be useful, 
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the 
* GNU Lesser General Public License for more details. 
* You should have received a copy of the GNU Lesser General Public License 
* along with CiviKey.  If not, see <http://www.gnu.org/licenses/>. 
*  
* Copyright © 2007-2012, 
*     Invenietis <http://www.invenietis.com>,
*     In’Tech INFO <http://www.intechinfo.fr>,
* All rights reserved. 
*-----------------------------------------------------------------------------*/
#endregion

using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace CK.Core
{
    /// <summary>
    /// Provides extension methods for <see cref="IActivityMonitor"/> to open groups and emit logs without tags.
    /// </summary>
    public static partial class ActivityMonitorExtension
    {
        #region IActivityMonitor.OpenGroup( ... )


        /// <summary>
        /// Opens a log level. <see cref="IActivityMonitor.CloseGroup">CloseGroup</see> must be called in order to
        /// close the group, or the returned object must be disposed.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="level">The log level of the group.</param>
        /// <param name="text">The text associated to the opening of the log.</param>
        /// <param name="fileName">The source code file name from which the group is opened. Injected by the compiler thanks to the attribute.</param>
        /// <param name="lineNumber">The line number in the source from which the group is opened. Injected by the compiler thanks to the attribute.</param>
        /// <returns>A disposable object that can be used to close the group.</returns>
        public static IDisposable OpenGroup( this IActivityMonitor @this, LogLevel level, string text )
        {
            return FilteredGroup( @this, level & LogLevel.Mask ) ?? @this.UnfilteredOpenGroup( ActivityMonitor.EmptyTag, level | LogLevel.IsFiltered, null, text, DateTime.UtcNow, null );
        }

        /// <summary>
        /// Opens a log level. <see cref="IActivityMonitor.CloseGroup">CloseGroup</see> must be called in order to
        /// close the group, or the returned object must be disposed.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="level">The log level of the group.</param>
        /// <param name="format">A format with one placeholder for the group title.</param>
        /// <param name="arg0">Parameter to format (placeholder {0}).</param>
        /// <param name="fileName">The source code file name from which the group is opened. Injected by the compiler thanks to the attribute.</param>
        /// <param name="lineNumber">The line number in the source from which the group is opened. Injected by the compiler thanks to the attribute.</param>
        /// <returns>A disposable object that can be used to close the group.</returns>
        public static IDisposable OpenGroup( this IActivityMonitor @this, LogLevel level, string format, object arg0 )
        {
            return FilteredGroup( @this, level & LogLevel.Mask ) ?? @this.UnfilteredOpenGroup( ActivityMonitor.EmptyTag, level | LogLevel.IsFiltered, null, String.Format( format, arg0 ), DateTime.UtcNow, null );
        }

        /// <summary>
        /// Opens a log level. <see cref="IActivityMonitor.CloseGroup">CloseGroup</see> must be called in order to
        /// close the group, or the returned object must be disposed.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="level">The log level of the group.</param>
        /// <param name="format">A format with two placeholders for the group title.</param>
        /// <param name="arg0">Parameter to format (placeholder {0}).</param>
        /// <param name="arg1">Parameter to format (placeholder {1}).</param>
        /// <param name="fileName">The source code file name from which the group is opened. Injected by the compiler thanks to the attribute.</param>
        /// <param name="lineNumber">The line number in the source from which the group is opened. Injected by the compiler thanks to the attribute.</param>
        /// <returns>A disposable object that can be used to close the group.</returns>
        public static IDisposable OpenGroup( this IActivityMonitor @this, LogLevel level, string format, object arg0, object arg1 )
        {
            return FilteredGroup( @this, level & LogLevel.Mask ) ?? @this.UnfilteredOpenGroup( ActivityMonitor.EmptyTag, level | LogLevel.IsFiltered, null, String.Format( format, arg0, arg1 ), DateTime.UtcNow, null );
        }

        /// <summary>
        /// Opens a log level. <see cref="IActivityMonitor.CloseGroup">CloseGroup</see> must be called in order to
        /// close the group, or the returned object must be disposed.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="level">The log level of the group.</param>
        /// <param name="format">A format with three placeholders for the group title.</param>
        /// <param name="arg0">Parameter to format (placeholder {0}).</param>
        /// <param name="arg1">Parameter to format (placeholder {1}).</param>
        /// <param name="arg2">Parameter to format (placeholder {2}).</param>
        /// <param name="fileName">The source code file name from which the group is opened. Injected by the compiler thanks to the attribute.</param>
        /// <param name="lineNumber">The line number in the source from which the group is opened. Injected by the compiler thanks to the attribute.</param>
        /// <returns>A disposable object that can be used to close the group.</returns>
        public static IDisposable OpenGroup( this IActivityMonitor @this, LogLevel level, string format, object arg0, object arg1, object arg2 )
        {
            return FilteredGroup( @this, level & LogLevel.Mask ) ?? @this.UnfilteredOpenGroup( ActivityMonitor.EmptyTag, level | LogLevel.IsFiltered, null, String.Format( format, arg0, arg1, arg2 ), DateTime.UtcNow, null );
        }

        /// <summary>
        /// Opens a log level. <see cref="IActivityMonitor.CloseGroup">CloseGroup</see> must be called in order to
        /// close the group, or the returned object must be disposed.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="level">The log level of the group.</param>
        /// <param name="format">A format with four placeholders for the group title.</param>
        /// <param name="arg0">Parameter to format (placeholder {0}).</param>
        /// <param name="arg1">Parameter to format (placeholder {1}).</param>
        /// <param name="arg2">Parameter to format (placeholder {2}).</param>
        /// <param name="arg3">Parameter to format (placeholder {3}).</param>
        /// <param name="fileName">The source code file name from which the group is opened. Injected by the compiler thanks to the attribute.</param>
        /// <param name="lineNumber">The line number in the source from which the group is opened. Injected by the compiler thanks to the attribute.</param>
        /// <returns>A disposable object that can be used to close the group.</returns>
        public static IDisposable OpenGroup( this IActivityMonitor @this, LogLevel level, string format, object arg0, object arg1, object arg2, object arg3 )
        {
            return FilteredGroup( @this, level & LogLevel.Mask ) ?? @this.UnfilteredOpenGroup( ActivityMonitor.EmptyTag, level | LogLevel.IsFiltered, null, String.Format( format, arg0, arg1, arg2, arg3 ), DateTime.UtcNow, null );
        }

        /// <summary>
        /// Opens a log level. <see cref="IActivityMonitor.CloseGroup">CloseGroup</see> must be called in order to
        /// close the group, or the returned object must be disposed.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="level">The log level of the group.</param>
        /// <param name="format">A format with five placeholders for the group title.</param>
        /// <param name="arg0">Parameter to format (placeholder {0}).</param>
        /// <param name="arg1">Parameter to format (placeholder {1}).</param>
        /// <param name="arg2">Parameter to format (placeholder {2}).</param>
        /// <param name="arg3">Parameter to format (placeholder {3}).</param>
        /// <param name="arg4">Parameter to format (placeholder {4}).</param>
        /// <param name="fileName">The source code file name from which the group is opened. Injected by the compiler thanks to the attribute.</param>
        /// <param name="lineNumber">The line number in the source from which the group is opened. Injected by the compiler thanks to the attribute.</param>
        /// <returns>A disposable object that can be used to close the group.</returns>
        public static IDisposable OpenGroup( this IActivityMonitor @this, LogLevel level, string format, object arg0, object arg1, object arg2, object arg3, object arg4 )
        {
            return FilteredGroup( @this, level & LogLevel.Mask ) ?? @this.UnfilteredOpenGroup( ActivityMonitor.EmptyTag, level | LogLevel.IsFiltered, null, String.Format( format, arg0, arg1, arg2, arg3, arg4 ), DateTime.UtcNow, null );
        }

        /// <summary>
        /// Opens a log level. <see cref="IActivityMonitor.CloseGroup">CloseGroup</see> must be called in order to
        /// close the group, or the returned object must be disposed.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="level">Log level. Since we are opening a group, the current <see cref="IActivityMonitor.ActualFilter">Filter</see> is ignored.</param>
        /// <param name="format">Format of the string.</param>
        /// <param name="arguments">Arguments to format.</param>
        /// <param name="fileName">The source code file name from which the group is opened. Injected by the compiler thanks to the attribute.</param>
        /// <param name="lineNumber">The line number in the source from which the group is opened. Injected by the compiler thanks to the attribute.</param>
        /// <returns>A disposable object that can be used to close the group.</returns>
        /// <remarks>
        /// A group opening is not be filtered since any subordinated logs may occur.
        /// It is left to the implementation to handle (or not) filtering when <see cref="IActivityMonitor.CloseGroup">CloseGroup</see> is called.
        /// </remarks>
        public static IDisposable OpenGroup( this IActivityMonitor @this, LogLevel level, string format, params object[] arguments )
        {
            return FilteredGroup( @this, level & LogLevel.Mask ) ?? @this.UnfilteredOpenGroup( ActivityMonitor.EmptyTag, level | LogLevel.IsFiltered, null, String.Format( format, arguments ), DateTime.UtcNow, null );
        }

        #region With getConclusionText

        /// <summary>
        /// Opens a log level. <see cref="IActivityMonitor.CloseGroup">CloseGroup</see> must be called in order to
        /// close the group, or the returned object must be disposed.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="level">Log level. Since we are opening a group, the current <see cref="IActivityMonitor.ActualFilter">Filter</see> is ignored.</param>
        /// <param name="getConclusionText">Optional function that will be called on group closing.</param>
        /// <param name="text">Group title.</param>
        /// <param name="fileName">The source code file name from which the group is opened. Injected by the compiler thanks to the attribute.</param>
        /// <param name="lineNumber">The line number in the source from which the group is opened. Injected by the compiler thanks to the attribute.</param>
        /// <returns>A disposable object that can be used to close the group.</returns>
        /// <remarks>
        /// A group opening is not be filtered since any subordinated logs may occur.
        /// It is left to the implementation to handle (or not) filtering when <see cref="IActivityMonitor.CloseGroup">CloseGroup</see> is called.
        /// </remarks>
        public static IDisposable OpenGroup( this IActivityMonitor @this, LogLevel level, Func<string> getConclusionText, string text )
        {
            return FilteredGroup( @this, level & LogLevel.Mask ) ?? @this.UnfilteredOpenGroup( ActivityMonitor.EmptyTag, level | LogLevel.IsFiltered, getConclusionText, text, DateTime.UtcNow, null );
        }

        /// <summary>
        /// Opens a log level. <see cref="IActivityMonitor.CloseGroup">CloseGroup</see> must be called in order to
        /// close the group, or the returned object must be disposed.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="level">Log level. Since we are opening a group, the current <see cref="IActivityMonitor.ActualFilter">Filter</see> is ignored.</param>
        /// <param name="getConclusionText">Optional function that will be called on group closing.</param>
        /// <param name="format">A format with one placeholder for the group title.</param>
        /// <param name="arg0">Parameter to format (placeholder {0}).</param>
        /// <param name="fileName">The source code file name from which the group is opened. Injected by the compiler thanks to the attribute.</param>
        /// <param name="lineNumber">The line number in the source from which the group is opened. Injected by the compiler thanks to the attribute.</param>
        /// <returns>A disposable object that can be used to close the group.</returns>
        /// <remarks>
        /// A group opening is not be filtered since any subordinated logs may occur.
        /// It is left to the implementation to handle (or not) filtering when <see cref="IActivityMonitor.CloseGroup">CloseGroup</see> is called.
        /// </remarks>
        public static IDisposable OpenGroup( this IActivityMonitor @this, LogLevel level, Func<string> getConclusionText, string format, object arg0 )
        {
            return FilteredGroup( @this, level & LogLevel.Mask ) ?? @this.UnfilteredOpenGroup( ActivityMonitor.EmptyTag, level | LogLevel.IsFiltered, getConclusionText, String.Format( format, arg0 ), DateTime.UtcNow, null );
        }

        /// <summary>
        /// Opens a log level. <see cref="IActivityMonitor.CloseGroup">CloseGroup</see> must be called in order to
        /// close the group, or the returned object must be disposed.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="level">Log level. Since we are opening a group, the current <see cref="IActivityMonitor.ActualFilter">Filter</see> is ignored.</param>
        /// <param name="getConclusionText">Optional function that will be called on group closing.</param>
        /// <param name="format">A format with two placeholders for the group title.</param>
        /// <param name="arg0">Parameter to format (placeholder {0}).</param>
        /// <param name="arg1">Parameter to format (placeholder {1}).</param>
        /// <param name="fileName">The source code file name from which the group is opened. Injected by the compiler thanks to the attribute.</param>
        /// <param name="lineNumber">The line number in the source from which the group is opened. Injected by the compiler thanks to the attribute.</param>
        /// <returns>A disposable object that can be used to close the group.</returns>
        /// <remarks>
        /// A group opening is not be filtered since any subordinated logs may occur.
        /// It is left to the implementation to handle (or not) filtering when <see cref="IActivityMonitor.CloseGroup">CloseGroup</see> is called.
        /// </remarks>
        public static IDisposable OpenGroup( this IActivityMonitor @this, LogLevel level, Func<string> getConclusionText, string format, object arg0, object arg1 )
        {
            return FilteredGroup( @this, level & LogLevel.Mask ) ?? @this.UnfilteredOpenGroup( ActivityMonitor.EmptyTag, level | LogLevel.IsFiltered, getConclusionText, String.Format( format, arg0, arg1 ), DateTime.UtcNow, null );
        }

        /// <summary>
        /// Opens a log level. <see cref="IActivityMonitor.CloseGroup">CloseGroup</see> must be called in order to
        /// close the group, or the returned object must be disposed.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="level">Log level. Since we are opening a group, the current <see cref="IActivityMonitor.ActualFilter">Filter</see> is ignored.</param>
        /// <param name="getConclusionText">Optional function that will be called on group closing.</param>
        /// <param name="format">A format with three placeholders for the group title.</param>
        /// <param name="arg0">Parameter to format (placeholder {0}).</param>
        /// <param name="arg1">Parameter to format (placeholder {1}).</param>
        /// <param name="arg2">Parameter to format (placeholder {2}).</param>
        /// <param name="fileName">The source code file name from which the group is opened. Injected by the compiler thanks to the attribute.</param>
        /// <param name="lineNumber">The line number in the source from which the group is opened. Injected by the compiler thanks to the attribute.</param>
        /// <returns>A disposable object that can be used to close the group.</returns>
        /// <remarks>
        /// A group opening is not be filtered since any subordinated logs may occur.
        /// It is left to the implementation to handle (or not) filtering when <see cref="IActivityMonitor.CloseGroup">CloseGroup</see> is called.
        /// </remarks>
        public static IDisposable OpenGroup( this IActivityMonitor @this, LogLevel level, Func<string> getConclusionText, string format, object arg0, object arg1, object arg2 )
        {
            return FilteredGroup( @this, level & LogLevel.Mask ) ?? @this.UnfilteredOpenGroup( ActivityMonitor.EmptyTag, level | LogLevel.IsFiltered, getConclusionText, String.Format( format, arg0, arg1, arg2 ), DateTime.UtcNow, null );
        }

        /// <summary>
        /// Opens a log level. <see cref="IActivityMonitor.CloseGroup">CloseGroup</see> must be called in order to
        /// close the group, or the returned object must be disposed.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="level">Log level. Since we are opening a group, the current <see cref="IActivityMonitor.ActualFilter">Filter</see> is ignored.</param>
        /// <param name="getConclusionText">Optional function that will be called on group closing.</param>
        /// <param name="format">A format with four placeholders for the group title.</param>
        /// <param name="arg0">Parameter to format (placeholder {0}).</param>
        /// <param name="arg1">Parameter to format (placeholder {1}).</param>
        /// <param name="arg2">Parameter to format (placeholder {2}).</param>
        /// <param name="arg3">Parameter to format (placeholder {3}).</param>
        /// <param name="fileName">The source code file name from which the group is opened. Injected by the compiler thanks to the attribute.</param>
        /// <param name="lineNumber">The line number in the source from which the group is opened. Injected by the compiler thanks to the attribute.</param>
        /// <returns>A disposable object that can be used to close the group.</returns>
        /// <remarks>
        /// A group opening is not be filtered since any subordinated logs may occur.
        /// It is left to the implementation to handle (or not) filtering when <see cref="IActivityMonitor.CloseGroup">CloseGroup</see> is called.
        /// </remarks>
        public static IDisposable OpenGroup( this IActivityMonitor @this, LogLevel level, Func<string> getConclusionText, string format, object arg0, object arg1, object arg2, object arg3 )
        {
            return FilteredGroup( @this, level & LogLevel.Mask ) ?? @this.UnfilteredOpenGroup( ActivityMonitor.EmptyTag, level | LogLevel.IsFiltered, getConclusionText, String.Format( format, arg0, arg1, arg2, arg3 ), DateTime.UtcNow, null );
        }

        /// <summary>
        /// Opens a log level. <see cref="IActivityMonitor.CloseGroup">CloseGroup</see> must be called in order to
        /// close the group, or the returned object must be disposed.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="level">Log level. Since we are opening a group, the current <see cref="IActivityMonitor.ActualFilter">Filter</see> is ignored.</param>
        /// <param name="getConclusionText">Optional function that will be called on group closing.</param>
        /// <param name="format">A format with five placeholders for the group title.</param>
        /// <param name="arg0">Parameter to format (placeholder {0}).</param>
        /// <param name="arg1">Parameter to format (placeholder {1}).</param>
        /// <param name="arg2">Parameter to format (placeholder {2}).</param>
        /// <param name="arg3">Parameter to format (placeholder {3}).</param>
        /// <param name="arg4">Parameter to format (placeholder {4}).</param>
        /// <param name="fileName">The source code file name from which the group is opened. Injected by the compiler thanks to the attribute.</param>
        /// <param name="lineNumber">The line number in the source from which the group is opened. Injected by the compiler thanks to the attribute.</param>
        /// <returns>A disposable object that can be used to close the group.</returns>
        /// <remarks>
        /// A group opening is not be filtered since any subordinated logs may occur.
        /// It is left to the implementation to handle (or not) filtering when <see cref="IActivityMonitor.CloseGroup">CloseGroup</see> is called.
        /// </remarks>
        public static IDisposable OpenGroup( this IActivityMonitor @this, LogLevel level, Func<string> getConclusionText, string format, object arg0, object arg1, object arg2, object arg3, object arg4 )
        {
            return FilteredGroup( @this, level & LogLevel.Mask ) ?? @this.UnfilteredOpenGroup( ActivityMonitor.EmptyTag, level | LogLevel.IsFiltered, getConclusionText, String.Format( format, arg0, arg1, arg2, arg3, arg4 ), DateTime.UtcNow, null );
        }

        /// <summary>
        /// Opens a log level. <see cref="IActivityMonitor.CloseGroup">CloseGroup</see> must be called in order to
        /// close the group, or the returned object must be disposed.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="level">Log level. Since we are opening a group, the current <see cref="IActivityMonitor.ActualFilter">Filter</see> is ignored.</param>
        /// <param name="getConclusionText">Optional function that will be called on group closing.</param>
        /// <param name="format">A multiple placeholders format for the group title.</param>
        /// <param name="arguments">Arguments to format.</param>
        /// <param name="fileName">The source code file name from which the group is opened. Injected by the compiler thanks to the attribute.</param>
        /// <param name="lineNumber">The line number in the source from which the group is opened. Injected by the compiler thanks to the attribute.</param>
        /// <returns>A disposable object that can be used to close the group.</returns>
        /// <remarks>
        /// A group opening is not be filtered since any subordinated logs may occur.
        /// It is left to the implementation to handle (or not) filtering when <see cref="IActivityMonitor.CloseGroup">CloseGroup</see> is called.
        /// </remarks>
        public static IDisposable OpenGroup( this IActivityMonitor @this, LogLevel level, Func<string> getConclusionText, string format, object[] arguments )
        {
            return FilteredGroup( @this, level & LogLevel.Mask ) ?? @this.UnfilteredOpenGroup( ActivityMonitor.EmptyTag, level | LogLevel.IsFiltered, getConclusionText, String.Format( format, arguments ), DateTime.UtcNow, null );
        }

        #endregion

        #endregion

        #region IActivityMonitor Trace(...), Info(...), Warn(...), Error(...) and Fatal(...).

        #region Trace

        /// <summary>
        /// Logs the text if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevel.Trace"/> or above.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="text">Text to log as a trace.</param>
        /// <param name="fileName">The source code file name from which the log is emitted. Injected by the compiler thanks to the attribute.</param>
        /// <param name="lineNumber">The line number in the source from which the log is emitted. Injected by the compiler thanks to the attribute.</param>
        public static void Trace( this IActivityMonitor @this, string text )
        {
            if( ShouldLogLine( @this, LogLevel.Trace ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Trace | LogLevel.IsFiltered, text, DateTime.UtcNow, null );
        }

        /// <summary>
        /// Logs a formatted text with one placeholder/parameter if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevel.Trace"/> or above.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="format">Text format to log as a trace.</param>
        /// <param name="arg0">Parameter to format (placeholder {0}).</param>

        public static void Trace( this IActivityMonitor @this, string format, object arg0 )
        {
            if( ShouldLogLine( @this, LogLevel.Trace ) )
            {
                if( arg0 is Exception ) throw new ArgumentException( R.PossibleWrongOverloadUseWithException, "arg0" );
                @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Trace | LogLevel.IsFiltered, String.Format( format, arg0 ), DateTime.UtcNow, null );
            }
        }

        /// <summary>
        /// Logs a formatted text with two placeholders/parameters if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevel.Trace"/> or above.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="format">Text format to log as a trace.</param>
        /// <param name="arg0">First parameter to format (placeholder {0}).</param>
        /// <param name="arg1">Second parameter to format (placeholder {1}).</param>

        public static void Trace( this IActivityMonitor @this, string format, object arg0, object arg1 )
        {
            if( ShouldLogLine( @this, LogLevel.Trace ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Trace | LogLevel.IsFiltered, String.Format( format, arg0, arg1 ), DateTime.UtcNow, null );
        }

        /// <summary>
        /// Logs a formatted text with three placeholders/parameters if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevel.Trace"/> or above.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="format">Text format to log as a trace.</param>
        /// <param name="arg0">First parameter to format (placeholder {0}).</param>
        /// <param name="arg1">Second parameter to format (placeholder {1}).</param>
        /// <param name="arg2">Third parameter to format (placeholder {2}).</param>

        public static void Trace( this IActivityMonitor @this, string format, object arg0, object arg1, object arg2 )
        {
            if( ShouldLogLine( @this, LogLevel.Trace ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Trace | LogLevel.IsFiltered, String.Format( format, arg0, arg1, arg2 ), DateTime.UtcNow, null );
        }

        /// <summary>
        /// Logs a formatted text with placeholders/parameters if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevel.Trace"/> or above.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="format">Text format to log as a trace.</param>
        /// <param name="args">Multiple parameters to format.</param>

        public static void Trace( this IActivityMonitor @this, string format, params object[] args )
        {
            if( ShouldLogLine( @this, LogLevel.Trace ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Trace | LogLevel.IsFiltered, String.Format( format, args ), DateTime.UtcNow, null );
        }

        /// <summary>
        /// Logs a formatted text by calling a delegate if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevel.Trace"/> or above.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="text">Delegate that returns a string.</param>

        public static void Trace( this IActivityMonitor @this, Func<string> text )
        {
            if( ShouldLogLine( @this, LogLevel.Trace ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Trace | LogLevel.IsFiltered, text(), DateTime.UtcNow, null );
        }

        /// <summary>
        /// Logs a formatted text by calling a delegate if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevel.Trace"/> or above.
        /// </summary>
        /// <typeparam name="T">Type of the parameter that <paramref name="text"/> accepts.</typeparam>
        /// <param name="param">Parameter of the <paramref name="text"/> delegate.</param>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="text">Delegate that returns a string.</param>
        public static void Trace<T>( this IActivityMonitor @this, T param, Func<T, string> text )
        {
            if( ShouldLogLine( @this, LogLevel.Trace ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Trace | LogLevel.IsFiltered, text( param ), DateTime.UtcNow, null );
        }

        /// <summary>
        /// Logs a formatted text by calling a delegate if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevel.Trace"/> or above.
        /// </summary>
        /// <typeparam name="T1">Type of the first parameter that <paramref name="text"/> accepts.</typeparam>
        /// <typeparam name="T2">Type of the second parameter that <paramref name="text"/> accepts.</typeparam>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="param1">First parameter for the <paramref name="text"/> delegate.</param>
        /// <param name="param2">Second parameter for the <paramref name="text"/> delegate.</param>
        /// <param name="text">Delegate that returns a string.</param>
        public static void Trace<T1, T2>( this IActivityMonitor @this, T1 param1, T2 param2, Func<T1, T2, string> text )
        {
            if( ShouldLogLine( @this, LogLevel.Trace ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Trace | LogLevel.IsFiltered, text( param1, param2 ), DateTime.UtcNow, null );
        }
        #endregion

        #region Info

        /// <summary>
        /// Logs the text if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevel.Info"/> or above.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="text">Text to log as an info.</param>

        public static void Info( this IActivityMonitor @this, string text )
        {
            if( ShouldLogLine( @this, LogLevel.Info ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Info | LogLevel.IsFiltered, text, DateTime.UtcNow, null );
        }

        /// <summary>
        /// Logs a formatted text with one placeholder/parameter if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevel.Info"/> or above.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="format">Text format to log as an info.</param>
        /// <param name="arg0">Parameter to format (placeholder {0}).</param>

        public static void Info( this IActivityMonitor @this, string format, object arg0 )
        {
            if( ShouldLogLine( @this, LogLevel.Info ) )
            {
                if( arg0 is Exception ) throw new ArgumentException( R.PossibleWrongOverloadUseWithException, "arg0" );
                @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Info | LogLevel.IsFiltered, String.Format( format, arg0 ), DateTime.UtcNow, null );
            }
        }

        /// <summary>
        /// Logs a formatted text with two placeholders/parameters if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevel.Info"/> or above.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="format">Text format to log as an info.</param>
        /// <param name="arg0">First parameter to format (placeholder {0}).</param>
        /// <param name="arg1">Second parameter to format (placeholder {1}).</param>

        public static void Info( this IActivityMonitor @this, string format, object arg0, object arg1 )
        {
            if( ShouldLogLine( @this, LogLevel.Info ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Info | LogLevel.IsFiltered, String.Format( format, arg0, arg1 ), DateTime.UtcNow, null );
        }

        /// <summary>
        /// Logs a formatted text with three placeholders/parameters if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevel.Info"/> or above.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="format">Text format to log as an info.</param>
        /// <param name="arg0">First parameter to format (placeholder {0}).</param>
        /// <param name="arg1">Second parameter to format (placeholder {1}).</param>
        /// <param name="arg2">Third parameter to format (placeholder {2}).</param>

        public static void Info( this IActivityMonitor @this, string format, object arg0, object arg1, object arg2 )
        {
            if( ShouldLogLine( @this, LogLevel.Info ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Info | LogLevel.IsFiltered, String.Format( format, arg0, arg1, arg2 ), DateTime.UtcNow, null );
        }

        /// <summary>
        /// Logs a formatted text with placeholders/parameters if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevel.Info"/> or above.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="format">Text format to log as an info.</param>
        /// <param name="args">Multiple parameters to format.</param>

        public static void Info( this IActivityMonitor @this, string format, params object[] args )
        {
            if( ShouldLogLine( @this, LogLevel.Info ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Info | LogLevel.IsFiltered, String.Format( format, args ), DateTime.UtcNow, null );
        }

        /// <summary>
        /// Logs a formatted text by calling a delegate if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevel.Info"/> or above.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="text">Delegate that returns a string.</param>

        public static void Info( this IActivityMonitor @this, Func<string> text )
        {
            if( ShouldLogLine( @this, LogLevel.Info ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Info | LogLevel.IsFiltered, text(), DateTime.UtcNow, null );
        }

        /// <summary>
        /// Logs a formatted text by calling a delegate if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevel.Info"/> or above.
        /// </summary>
        /// <typeparam name="T">Type of the parameter that <paramref name="text"/> accepts.</typeparam>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="param">Parameter of the <paramref name="text"/> delegate.</param>
        /// <param name="text">Delegate that returns a string.</param>

        public static void Info<T>( this IActivityMonitor @this, T param, Func<T, string> text )
        {
            if( ShouldLogLine( @this, LogLevel.Info ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Info | LogLevel.IsFiltered, text( param ), DateTime.UtcNow, null );
        }

        /// <summary>
        /// Logs a formatted text by calling a delegate if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevel.Info"/> or above.
        /// </summary>
        /// <typeparam name="T1">Type of the first parameter that <paramref name="text"/> accepts.</typeparam>
        /// <typeparam name="T2">Type of the second parameter that <paramref name="text"/> accepts.</typeparam>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="param1">First parameter for the <paramref name="text"/> delegate.</param>
        /// <param name="param2">Second parameter for the <paramref name="text"/> delegate.</param>
        /// <param name="text">Delegate that returns a string.</param>

        public static void Info<T1, T2>( this IActivityMonitor @this, T1 param1, T2 param2, Func<T1, T2, string> text )
        {
            if( ShouldLogLine( @this, LogLevel.Info ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Info | LogLevel.IsFiltered, text( param1, param2 ), DateTime.UtcNow, null );
        }
        #endregion

        #region Warn

        /// <summary>
        /// Logs the text if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevel.Warn"/> or above.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="text">Text to log as a warning.</param>

        public static void Warn( this IActivityMonitor @this, string text )
        {
            if( ShouldLogLine( @this, LogLevel.Warn ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Warn | LogLevel.IsFiltered, text, DateTime.UtcNow, null );
        }

        /// <summary>
        /// Logs a formatted text with one placeholder/parameter if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevel.Warn"/> or above.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="format">Text format to log as a warning.</param>
        /// <param name="arg0">Parameter to format (placeholder {0}).</param>

        public static void Warn( this IActivityMonitor @this, string format, object arg0 )
        {
            if( ShouldLogLine( @this, LogLevel.Warn ) )
            {
                if( arg0 is Exception ) throw new ArgumentException( R.PossibleWrongOverloadUseWithException, "arg0" );
                @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Warn | LogLevel.IsFiltered, String.Format( format, arg0 ), DateTime.UtcNow, null );
            }
        }

        /// <summary>
        /// Logs a formatted text with two placeholders/parameters if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevel.Warn"/> or above.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="format">Text format to log as a warning.</param>
        /// <param name="arg0">First parameter to format (placeholder {0}).</param>
        /// <param name="arg1">Second parameter to format (placeholder {1}).</param>

        public static void Warn( this IActivityMonitor @this, string format, object arg0, object arg1 )
        {
            if( ShouldLogLine( @this, LogLevel.Warn ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Warn | LogLevel.IsFiltered, String.Format( format, arg0, arg1 ), DateTime.UtcNow, null );
        }

        /// <summary>
        /// Logs a formatted text with three placeholders/parameters if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevel.Warn"/> or above.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="format">Text format to log as a warning.</param>
        /// <param name="arg0">First parameter to format (placeholder {0}).</param>
        /// <param name="arg1">Second parameter to format (placeholder {1}).</param>
        /// <param name="arg2">Third parameter to format (placeholder {2}).</param>

        public static void Warn( this IActivityMonitor @this, string format, object arg0, object arg1, object arg2 )
        {
            if( ShouldLogLine( @this, LogLevel.Warn ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Warn | LogLevel.IsFiltered, String.Format( format, arg0, arg1, arg2 ), DateTime.UtcNow, null );
        }

        /// <summary>
        /// Logs a formatted text with placeholders/parameters if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevel.Warn"/> or above.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="format">Text format to log as a warning.</param>
        /// <param name="args">Multiple parameters to format.</param>

        public static void Warn( this IActivityMonitor @this, string format, params object[] args )
        {
            if( ShouldLogLine( @this, LogLevel.Warn ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Warn | LogLevel.IsFiltered, String.Format( format, args ), DateTime.UtcNow, null );
        }

        /// <summary>
        /// Logs a formatted text by calling a delegate if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevel.Warn"/> or above.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="text">Delegate that returns a string.</param>

        public static void Warn( this IActivityMonitor @this, Func<string> text )
        {
            if( ShouldLogLine( @this, LogLevel.Warn ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Warn | LogLevel.IsFiltered, text(), DateTime.UtcNow, null );
        }

        /// <summary>
        /// Logs a formatted text by calling a delegate if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevel.Warn"/> or above.
        /// </summary>
        /// <typeparam name="T">Type of the parameter that <paramref name="text"/> accepts.</typeparam>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="param">Parameter of the <paramref name="text"/> delegate.</param>
        /// <param name="text">Delegate that returns a string.</param>

        public static void Warn<T>( this IActivityMonitor @this, T param, Func<T, string> text )
        {
            if( ShouldLogLine( @this, LogLevel.Warn ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Warn | LogLevel.IsFiltered, text( param ), DateTime.UtcNow, null );
        }

        /// <summary>
        /// Logs a formatted text by calling a delegate if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevel.Warn"/> or above.
        /// </summary>
        /// <typeparam name="T1">Type of the first parameter that <paramref name="text"/> accepts.</typeparam>
        /// <typeparam name="T2">Type of the second parameter that <paramref name="text"/> accepts.</typeparam>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="param1">First parameter for the <paramref name="text"/> delegate.</param>
        /// <param name="param2">Second parameter for the <paramref name="text"/> delegate.</param>
        /// <param name="text">Delegate that returns a string.</param>

        public static void Warn<T1, T2>( this IActivityMonitor @this, T1 param1, T2 param2, Func<T1, T2, string> text )
        {
            if( ShouldLogLine( @this, LogLevel.Warn ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Warn | LogLevel.IsFiltered, text( param1, param2 ), DateTime.UtcNow, null );
        }
        #endregion

        #region Error

        /// <summary>
        /// Logs the text if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevel.Error"/> or above.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="text">Text to log as an error.</param>

        public static void Error( this IActivityMonitor @this, string text )
        {
            if( ShouldLogLine( @this, LogLevel.Error ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Error | LogLevel.IsFiltered, text, DateTime.UtcNow, null );
        }

        /// <summary>
        /// Logs a formatted text with one placeholder/parameter if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevel.Error"/> or above.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="format">Text format to log as an error.</param>
        /// <param name="arg0">Parameter to format (placeholder {0}).</param>

        public static void Error( this IActivityMonitor @this, string format, object arg0 )
        {
            if( ShouldLogLine( @this, LogLevel.Error ) )
            {
                if( arg0 is Exception ) throw new ArgumentException( R.PossibleWrongOverloadUseWithException, "arg0" );
                @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Error | LogLevel.IsFiltered, String.Format( format, arg0 ), DateTime.UtcNow, null );
            }
        }

        /// <summary>
        /// Logs a formatted text with two placeholders/parameters if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevel.Error"/> or above.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="format">Text format to log as an error.</param>
        /// <param name="arg0">First parameter to format (placeholder {0}).</param>
        /// <param name="arg1">Second parameter to format (placeholder {1}).</param>

        public static void Error( this IActivityMonitor @this, string format, object arg0, object arg1 )
        {
            if( ShouldLogLine( @this, LogLevel.Error ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Error | LogLevel.IsFiltered, String.Format( format, arg0, arg1 ), DateTime.UtcNow, null );
        }

        /// <summary>
        /// Logs a formatted text with three placeholders/parameters if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevel.Error"/> or above.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="format">Text format to log as an error.</param>
        /// <param name="arg0">First parameter to format (placeholder {0}).</param>
        /// <param name="arg1">Second parameter to format (placeholder {1}).</param>
        /// <param name="arg2">Third parameter to format (placeholder {2}).</param>

        public static void Error( this IActivityMonitor @this, string format, object arg0, object arg1, object arg2 )
        {
            if( ShouldLogLine( @this, LogLevel.Error ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Error | LogLevel.IsFiltered, String.Format( format, arg0, arg1, arg2 ), DateTime.UtcNow, null );
        }

        /// <summary>
        /// Logs a formatted text with placeholders/parameters if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevel.Error"/> or above.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="format">Text format to log as an error.</param>
        /// <param name="args">Multiple parameters to format.</param>

        public static void Error( this IActivityMonitor @this, string format, params object[] args )
        {
            if( ShouldLogLine( @this, LogLevel.Error ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Error | LogLevel.IsFiltered, String.Format( format, args ), DateTime.UtcNow, null );
        }

        /// <summary>
        /// Logs a formatted text by calling a delegate if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevel.Error"/> or above.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="text">Delegate that returns a string.</param>

        public static void Error( this IActivityMonitor @this, Func<string> text )
        {
            if( ShouldLogLine( @this, LogLevel.Error ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Error | LogLevel.IsFiltered, text(), DateTime.UtcNow, null );
        }

        /// <summary>
        /// Logs a formatted text by calling a delegate if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevel.Error"/> or above.
        /// </summary>
        /// <typeparam name="T">Type of the parameter that <paramref name="text"/> accepts.</typeparam>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="param">Parameter of the <paramref name="text"/> delegate.</param>
        /// <param name="text">Delegate that returns a string.</param>

        public static void Error<T>( this IActivityMonitor @this, T param, Func<T, string> text )
        {
            if( ShouldLogLine( @this, LogLevel.Error ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Error | LogLevel.IsFiltered, text( param ), DateTime.UtcNow, null );
        }

        /// <summary>
        /// Logs a formatted text by calling a delegate if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevel.Error"/> or above.
        /// </summary>
        /// <typeparam name="T1">Type of the first parameter that <paramref name="text"/> accepts.</typeparam>
        /// <typeparam name="T2">Type of the second parameter that <paramref name="text"/> accepts.</typeparam>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="param1">First parameter for the <paramref name="text"/> delegate.</param>
        /// <param name="param2">Second parameter for the <paramref name="text"/> delegate.</param>
        /// <param name="text">Delegate that returns a string.</param>

        public static void Error<T1, T2>( this IActivityMonitor @this, T1 param1, T2 param2, Func<T1, T2, string> text )
        {
            if( ShouldLogLine( @this, LogLevel.Error ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Error | LogLevel.IsFiltered, text( param1, param2 ), DateTime.UtcNow, null );
        }
        #endregion

        #region Fatal

        /// <summary>
        /// Logs the text if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevel.Fatal"/> or above.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="text">Text to log as a fatal error.</param>

        public static void Fatal( this IActivityMonitor @this, string text )
        {
            if( ShouldLogLine( @this, LogLevel.Fatal ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Fatal | LogLevel.IsFiltered, text, DateTime.UtcNow, null );
        }

        /// <summary>
        /// Logs a formatted text with one placeholder/parameter if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevel.Fatal"/> or above.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="format">Text format to log as a fatal error.</param>
        /// <param name="arg0">Parameter to format (placeholder {0}).</param>

        public static void Fatal( this IActivityMonitor @this, string format, object arg0 )
        {
            if( ShouldLogLine( @this, LogLevel.Fatal ) )
            {
                if( arg0 is Exception ) throw new ArgumentException( R.PossibleWrongOverloadUseWithException, "arg0" );
                @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Fatal | LogLevel.IsFiltered, String.Format( format, arg0 ), DateTime.UtcNow, null );
            }
        }

        /// <summary>
        /// Logs a formatted text with two placeholders/parameters if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevel.Fatal"/> or above.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="format">Text format to log as a fatal error.</param>
        /// <param name="arg0">First parameter to format (placeholder {0}).</param>
        /// <param name="arg1">Second parameter to format (placeholder {1}).</param>

        public static void Fatal( this IActivityMonitor @this, string format, object arg0, object arg1 )
        {
            if( ShouldLogLine( @this, LogLevel.Fatal ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Fatal | LogLevel.IsFiltered, String.Format( format, arg0, arg1 ), DateTime.UtcNow, null );
        }

        /// <summary>
        /// Logs a formatted text with three placeholders/parameters if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevel.Fatal"/> or above.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="format">Text format to log as a fatal error.</param>
        /// <param name="arg0">First parameter to format (placeholder {0}).</param>
        /// <param name="arg1">Second parameter to format (placeholder {1}).</param>
        /// <param name="arg2">Third parameter to format (placeholder {2}).</param>

        public static void Fatal( this IActivityMonitor @this, string format, object arg0, object arg1, object arg2 )
        {
            if( ShouldLogLine( @this, LogLevel.Fatal ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Fatal | LogLevel.IsFiltered, String.Format( format, arg0, arg1, arg2 ), DateTime.UtcNow, null );
        }

        /// <summary>
        /// Logs a formatted text with placeholders/parameters if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevel.Fatal"/> or above.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="format">Text format to log as a fatal error.</param>
        /// <param name="args">Multiple parameters to format.</param>

        public static void Fatal( this IActivityMonitor @this, string format, params object[] args )
        {
            if( ShouldLogLine( @this, LogLevel.Fatal ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Fatal | LogLevel.IsFiltered, String.Format( format, args ), DateTime.UtcNow, null );
        }

        /// <summary>
        /// Logs a formatted text by calling a delegate if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevel.Fatal"/> or above.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="text">Delegate that returns a string.</param>

        public static void Fatal( this IActivityMonitor @this, Func<string> text )
        {
            if( ShouldLogLine( @this, LogLevel.Fatal ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Fatal | LogLevel.IsFiltered, text(), DateTime.UtcNow, null );
        }

        /// <summary>
        /// Logs a formatted text by calling a delegate if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevel.Fatal"/> or above.
        /// </summary>
        /// <typeparam name="T">Type of the parameter that <paramref name="text"/> accepts.</typeparam>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="param">Parameter of the <paramref name="text"/> delegate.</param>
        /// <param name="text">Delegate that returns a string.</param>
        public static IActivityMonitor Fatal<T>( this IActivityMonitor @this, T param, Func<T, string> text )
        {
            if( ShouldLogLine( @this, LogLevel.Fatal ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Fatal | LogLevel.IsFiltered, text( param ), DateTime.UtcNow, null );
            return @this;
        }

        /// <summary>
        /// Logs a formatted text by calling a delegate if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevel.Fatal"/> or above.
        /// </summary>
        /// <typeparam name="T1">Type of the first parameter that <paramref name="text"/> accepts.</typeparam>
        /// <typeparam name="T2">Type of the second parameter that <paramref name="text"/> accepts.</typeparam>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="param1">First parameter for the <paramref name="text"/> delegate.</param>
        /// <param name="param2">Second parameter for the <paramref name="text"/> delegate.</param>
        /// <param name="text">Delegate that returns a string.</param>
        public static void Fatal<T1, T2>( this IActivityMonitor @this, T1 param1, T2 param2, Func<T1, T2, string> text )
        {
            if( ShouldLogLine( @this, LogLevel.Fatal ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Fatal | LogLevel.IsFiltered, text( param1, param2 ), DateTime.UtcNow, null );
        }
        #endregion

        #endregion

        #region IActivityMonitor Trace( Exception ), Warn( Exception ), Error( Exception ), Fatal( Exception ), OpenGroup( Exception ).

        #region Trace

        /// <summary>
        /// Logs the exception as a trace.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="ex">The exception to log.</param>

        public static void Trace( this IActivityMonitor @this, Exception ex )
        {
            if( ShouldLogLine( @this, LogLevel.Trace ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Trace | LogLevel.IsFiltered, null, DateTime.UtcNow, ex );
        }

        /// <summary>
        /// Logs the exception as a trace.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="ex">The exception to log.</param>
        /// <param name="text">Text to log as a trace.</param>

        public static void Trace( this IActivityMonitor @this, Exception ex, string text )
        {
            if( ShouldLogLine( @this, LogLevel.Trace ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Trace | LogLevel.IsFiltered, text, DateTime.UtcNow, ex );
        }

        /// <summary>
        /// Logs the exception as a trace.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="ex">The exception to log.</param>
        /// <param name="format">Text format to log as a trace.</param>
        /// <param name="arg0">Parameter to format (placeholder {0}).</param>

        public static void Trace( this IActivityMonitor @this, Exception ex, string format, object arg0 )
        {
            if( ShouldLogLine( @this, LogLevel.Trace ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Trace | LogLevel.IsFiltered, String.Format( format, arg0 ), DateTime.UtcNow, ex );
        }

        /// <summary>
        /// Logs the exception as a trace.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="ex">The exception to log.</param>
        /// <param name="format">Text format to log as a trace.</param>
        /// <param name="arg0">First parameter to format (placeholder {0}).</param>
        /// <param name="arg1">Second parameter to format (placeholder {1}).</param>

        public static void Trace( this IActivityMonitor @this, Exception ex, string format, object arg0, object arg1 )
        {
            if( ShouldLogLine( @this, LogLevel.Trace ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Trace | LogLevel.IsFiltered, String.Format( format, arg0, arg1 ), DateTime.UtcNow, ex );
        }

        /// <summary>
        /// Logs the exception as a trace.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="ex">The exception to log.</param>
        /// <param name="format">Text format to log as a trace.</param>
        /// <param name="arg0">First parameter to format (placeholder {0}).</param>
        /// <param name="arg1">Second parameter to format (placeholder {1}).</param>
        /// <param name="arg2">Third parameter to format (placeholder {2}).</param>

        public static void Trace( this IActivityMonitor @this, Exception ex, string format, object arg0, object arg1, object arg2 )
        {
            if( ShouldLogLine( @this, LogLevel.Trace ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Trace | LogLevel.IsFiltered, String.Format( format, arg0, arg1, arg2 ), DateTime.UtcNow, ex );
        }

        /// <summary>
        /// Logs the exception as a trace.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="ex">The exception to log.</param>
        /// <param name="format">Text format to log as a trace.</param>
        /// <param name="args">Multiple parameters to format.</param>

        public static void Trace( this IActivityMonitor @this, Exception ex, string format, params object[] args )
        {
            if( ShouldLogLine( @this, LogLevel.Trace ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Trace | LogLevel.IsFiltered, String.Format( format, args ), DateTime.UtcNow, ex );
        }

        #endregion

        #region Info

        /// <summary>
        /// Logs the exception as an information if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevel.Info"/> or above.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="ex">The exception to log.</param>

        public static void Info( this IActivityMonitor @this, Exception ex )
        {
            if( ShouldLogLine( @this, LogLevel.Info ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Info | LogLevel.IsFiltered, null, DateTime.UtcNow, ex );
        }

        /// <summary>
        /// Logs the exception as an information if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevel.Info"/> or above.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="ex">The exception to log.</param>
        /// <param name="text">Text to log as an information.</param>

        public static void Info( this IActivityMonitor @this, Exception ex, string text )
        {
            if( ShouldLogLine( @this, LogLevel.Info ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Info | LogLevel.IsFiltered, text, DateTime.UtcNow, ex );
        }

        /// <summary>
        /// Logs the exception as an information if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevel.Info"/> or above.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="ex">The exception to log.</param>
        /// <param name="format">Text format to log as an information.</param>
        /// <param name="arg0">Parameter to format (placeholder {0}).</param>

        public static void Info( this IActivityMonitor @this, Exception ex, string format, object arg0 )
        {
            if( ShouldLogLine( @this, LogLevel.Info ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Info | LogLevel.IsFiltered, String.Format( format, arg0 ), DateTime.UtcNow, ex );
        }

        /// <summary>
        /// Logs the exception as an information if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevel.Info"/> or above.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="ex">The exception to log.</param>
        /// <param name="format">Text format to log as an information.</param>
        /// <param name="arg0">First parameter to format (placeholder {0}).</param>
        /// <param name="arg1">Second parameter to format (placeholder {1}).</param>

        public static void Info( this IActivityMonitor @this, Exception ex, string format, object arg0, object arg1 )
        {
            if( ShouldLogLine( @this, LogLevel.Info ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Info | LogLevel.IsFiltered, String.Format( format, arg0, arg1 ), DateTime.UtcNow, ex );
        }

        /// <summary>
        /// Logs the exception as an information if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevel.Info"/> or above.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="ex">The exception to log.</param>
        /// <param name="format">Text format to log as an information.</param>
        /// <param name="arg0">First parameter to format (placeholder {0}).</param>
        /// <param name="arg1">Second parameter to format (placeholder {1}).</param>
        /// <param name="arg2">Third parameter to format (placeholder {2}).</param>

        public static void Info( this IActivityMonitor @this, Exception ex, string format, object arg0, object arg1, object arg2 )
        {
            if( ShouldLogLine( @this, LogLevel.Info ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Info | LogLevel.IsFiltered, String.Format( format, arg0, arg1, arg2 ), DateTime.UtcNow, ex );
        }

        /// <summary>
        /// Logs the exception as an information if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevel.Info"/> or above.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="ex">The exception to log.</param>
        /// <param name="format">Text format to log as an information.</param>
        /// <param name="args">Multiple parameters to format.</param>

        public static void Info( this IActivityMonitor @this, Exception ex, string format, params object[] args )
        {
            if( ShouldLogLine( @this, LogLevel.Info ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Info | LogLevel.IsFiltered, String.Format( format, args ), DateTime.UtcNow, ex );
        }

        #endregion

        #region Warn

        /// <summary>
        /// Logs the exception as a warning if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevel.Warn"/> or above.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="ex">The exception to log.</param>

        public static void Warn( this IActivityMonitor @this, Exception ex )
        {
            if( ShouldLogLine( @this, LogLevel.Warn ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Warn | LogLevel.IsFiltered, null, DateTime.UtcNow, ex );
        }

        /// <summary>
        /// Logs the exception as a warning if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevel.Warn"/> or above.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="ex">The exception to log.</param>
        /// <param name="text">Text to log as a warning.</param>

        public static void Warn( this IActivityMonitor @this, Exception ex, string text )
        {
            if( ShouldLogLine( @this, LogLevel.Warn ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Warn | LogLevel.IsFiltered, text, DateTime.UtcNow, ex );
        }

        /// <summary>
        /// Logs the exception as a warning if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevel.Warn"/> or above.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="ex">The exception to log.</param>
        /// <param name="format">Text format to log as a warning.</param>
        /// <param name="arg0">Parameter to format (placeholder {0}).</param>

        public static void Warn( this IActivityMonitor @this, Exception ex, string format, object arg0 )
        {
            if( ShouldLogLine( @this, LogLevel.Warn ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Warn | LogLevel.IsFiltered, String.Format( format, arg0 ), DateTime.UtcNow, ex );
        }

        /// <summary>
        /// Logs the exception as a warning if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevel.Warn"/> or above.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="ex">The exception to log.</param>
        /// <param name="format">Text format to log as a warning.</param>
        /// <param name="arg0">First parameter to format (placeholder {0}).</param>
        /// <param name="arg1">Second parameter to format (placeholder {1}).</param>

        public static void Warn( this IActivityMonitor @this, Exception ex, string format, object arg0, object arg1 )
        {
            if( ShouldLogLine( @this, LogLevel.Warn ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Warn | LogLevel.IsFiltered, String.Format( format, arg0, arg1 ), DateTime.UtcNow, ex );
        }

        /// <summary>
        /// Logs the exception as a warning if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevel.Warn"/> or above.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="ex">The exception to log.</param>
        /// <param name="format">Text format to log as a warning.</param>
        /// <param name="arg0">First parameter to format (placeholder {0}).</param>
        /// <param name="arg1">Second parameter to format (placeholder {1}).</param>
        /// <param name="arg2">Third parameter to format (placeholder {2}).</param>

        public static void Warn( this IActivityMonitor @this, Exception ex, string format, object arg0, object arg1, object arg2 )
        {
            if( ShouldLogLine( @this, LogLevel.Warn ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Warn | LogLevel.IsFiltered, String.Format( format, arg0, arg1, arg2 ), DateTime.UtcNow, ex );
        }

        /// <summary>
        /// Logs the exception as a warning if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevel.Warn"/> or above.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="ex">The exception to log.</param>
        /// <param name="format">Text format to log as a warning.</param>
        /// <param name="args">Multiple parameters to format.</param>

        public static void Warn( this IActivityMonitor @this, Exception ex, string format, params object[] args )
        {
            if( ShouldLogLine( @this, LogLevel.Warn ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Warn | LogLevel.IsFiltered, String.Format( format, args ), DateTime.UtcNow, ex );
        }

        #endregion

        #region Error

        /// <summary>
        /// Logs the exception as an error if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevel.Error"/> or above.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="ex">The exception to log.</param>

        public static void Error( this IActivityMonitor @this, Exception ex )
        {
            if( ShouldLogLine( @this, LogLevel.Error ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Error | LogLevel.IsFiltered, null, DateTime.UtcNow, ex );
        }

        /// <summary>
        /// Logs the exception as an error if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevel.Error"/> or above.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="ex">The exception to log.</param>
        /// <param name="text">Text to log as an error.</param>

        public static void Error( this IActivityMonitor @this, Exception ex, string text )
        {
            if( ShouldLogLine( @this, LogLevel.Error ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Error | LogLevel.IsFiltered, text, DateTime.UtcNow, ex );
        }

        /// <summary>
        /// Logs the exception as an error if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevel.Error"/> or above.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="ex">The exception to log.</param>
        /// <param name="format">Text format to log as an error.</param>
        /// <param name="arg0">Parameter to format (placeholder {0}).</param>

        public static void Error( this IActivityMonitor @this, Exception ex, string format, object arg0 )
        {
            if( ShouldLogLine( @this, LogLevel.Error ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Error | LogLevel.IsFiltered, String.Format( format, arg0 ), DateTime.UtcNow, ex );
        }

        /// <summary>
        /// Logs the exception as an error if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevel.Error"/> or above.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="ex">The exception to log.</param>
        /// <param name="format">Text format to log as an error.</param>
        /// <param name="arg0">First parameter to format (placeholder {0}).</param>
        /// <param name="arg1">Second parameter to format (placeholder {1}).</param>

        public static void Error( this IActivityMonitor @this, Exception ex, string format, object arg0, object arg1 )
        {
            if( ShouldLogLine( @this, LogLevel.Error ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Error | LogLevel.IsFiltered, String.Format( format, arg0, arg1 ), DateTime.UtcNow, ex );
        }

        /// <summary>
        /// Logs the exception as an error if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevel.Error"/> or above.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="ex">The exception to log.</param>
        /// <param name="format">Text format to log as an error.</param>
        /// <param name="arg0">First parameter to format (placeholder {0}).</param>
        /// <param name="arg1">Second parameter to format (placeholder {1}).</param>
        /// <param name="arg2">Third parameter to format (placeholder {2}).</param>

        public static void Error( this IActivityMonitor @this, Exception ex, string format, object arg0, object arg1, object arg2 )
        {
            if( ShouldLogLine( @this, LogLevel.Error ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Error | LogLevel.IsFiltered, String.Format( format, arg0, arg1, arg2 ), DateTime.UtcNow, ex );
        }

        /// <summary>
        /// Logs the exception as an error if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevel.Error"/> or above.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="ex">The exception to log.</param>
        /// <param name="format">Text format to log as an error.</param>
        /// <param name="args">Multiple parameters to format.</param>

        public static void Error( this IActivityMonitor @this, Exception ex, string format, params object[] args )
        {
            if( ShouldLogLine( @this, LogLevel.Error ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Error | LogLevel.IsFiltered, String.Format( format, args ), DateTime.UtcNow, ex );
        }

        #endregion

        #region Fatal

        /// <summary>
        /// Logs the exception (except if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevelFilter.Off"/>).
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="ex">The exception to log.</param>

        public static void Fatal( this IActivityMonitor @this, Exception ex )
        {
            if( ShouldLogLine( @this, LogLevel.Fatal ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Fatal | LogLevel.IsFiltered, null, DateTime.UtcNow, ex );
        }

        /// <summary>
        /// Logs the exception (except if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevelFilter.Off"/>).
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="ex">The exception to log.</param>
        /// <param name="text">Text to log as a fatal error.</param>

        public static void Fatal( this IActivityMonitor @this, Exception ex, string text )
        {
            if( ShouldLogLine( @this, LogLevel.Fatal ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Fatal | LogLevel.IsFiltered, text, DateTime.UtcNow, ex );
        }

        /// <summary>
        /// Logs the exception (except if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevelFilter.Off"/>).
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="ex">The exception to log.</param>
        /// <param name="format">Text format to log as a fatal error.</param>
        /// <param name="arg0">Parameter to format (placeholder {0}).</param>

        public static void Fatal( this IActivityMonitor @this, Exception ex, string format, object arg0 )
        {
            if( ShouldLogLine( @this, LogLevel.Fatal ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Fatal | LogLevel.IsFiltered, String.Format( format, arg0 ), DateTime.UtcNow, ex );
        }

        /// <summary>
        /// Logs the exception (except if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevelFilter.Off"/>).
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="ex">The exception to log.</param>
        /// <param name="format">Text format to log as a fatal error.</param>
        /// <param name="arg0">First parameter to format (placeholder {0}).</param>
        /// <param name="arg1">Second parameter to format (placeholder {1}).</param>

        public static void Fatal( this IActivityMonitor @this, Exception ex, string format, object arg0, object arg1 )
        {
            if( ShouldLogLine( @this, LogLevel.Fatal ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Fatal | LogLevel.IsFiltered, String.Format( format, arg0, arg1 ), DateTime.UtcNow, ex );
        }

        /// <summary>
        /// Logs the exception (except if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevelFilter.Off"/>).
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="ex">The exception to log.</param>
        /// <param name="format">Text format to log as a fatal error.</param>
        /// <param name="arg0">First parameter to format (placeholder {0}).</param>
        /// <param name="arg1">Second parameter to format (placeholder {1}).</param>
        /// <param name="arg2">Third parameter to format (placeholder {2}).</param>

        public static void Fatal( this IActivityMonitor @this, Exception ex, string format, object arg0, object arg1, object arg2 )
        {
            if( ShouldLogLine( @this, LogLevel.Fatal ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Fatal | LogLevel.IsFiltered, String.Format( format, arg0, arg1, arg2 ), DateTime.UtcNow, ex );
        }

        /// <summary>
        /// Logs the exception (except if current <see cref="IActivityMonitor.ActualFilter"/> is <see cref="LogLevelFilter.Off"/>).
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="ex">The exception to log.</param>
        /// <param name="format">Text format to log as a fatal error.</param>
        /// <param name="args">Multiple parameters to format.</param>

        public static void Fatal( this IActivityMonitor @this, Exception ex, string format, params object[] args )
        {
            if( ShouldLogLine( @this, LogLevel.Fatal ) ) @this.UnfilteredLog( ActivityMonitor.EmptyTag, LogLevel.Fatal | LogLevel.IsFiltered, String.Format( format, args ), DateTime.UtcNow, ex );
        }

        #endregion

        #region OpenGroup

        /// <summary>
        /// Opens a log level associated to an <see cref="Exception"/>. <see cref="IActivityMonitor.CloseGroup">CloseGroup</see> must be called in order to
        /// close the group, or the returned object must be disposed.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="level">Log level. Since we are opening a group, the current <see cref="IActivityMonitor.ActualFilter">Filter</see> is ignored.</param>
        /// <param name="ex">The exception to log.</param>
        /// <returns>A disposable object that can be used to close the group.</returns>
        public static IDisposable OpenGroup( this IActivityMonitor @this, LogLevel level, Exception ex )
        {
            return FilteredGroup( @this, level & LogLevel.Mask ) ?? @this.UnfilteredOpenGroup( ActivityMonitor.EmptyTag, level | LogLevel.IsFiltered, null, null, DateTime.UtcNow, ex );
        }

        /// <summary>
        /// Opens a log level associated to an <see cref="Exception"/>. <see cref="IActivityMonitor.CloseGroup">CloseGroup</see> must be called in order to
        /// close the group, or the returned object must be disposed.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="level">Log level. Since we are opening a group, the current <see cref="IActivityMonitor.ActualFilter">Filter</see> is ignored.</param>
        /// <param name="ex">The exception to log.</param>
        /// <param name="text">The group title.</param>
        /// <returns>A disposable object that can be used to close the group.</returns>
        public static IDisposable OpenGroup( this IActivityMonitor @this, LogLevel level, Exception ex, string text )
        {
            return FilteredGroup( @this, level & LogLevel.Mask ) ?? @this.UnfilteredOpenGroup( ActivityMonitor.EmptyTag, level | LogLevel.IsFiltered, null, text, DateTime.UtcNow, ex );
        }

        /// <summary>
        /// Opens a log level associated to an <see cref="Exception"/>. <see cref="IActivityMonitor.CloseGroup">CloseGroup</see> must be called in order to
        /// close the group, or the returned object must be disposed.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="level">Log level. Since we are opening a group, the current <see cref="IActivityMonitor.ActualFilter">Filter</see> is ignored.</param>
        /// <param name="ex">Exception to log.</param>
        /// <param name="format">Text format for group title.</param>
        /// <param name="arg0">Parameter to format (placeholder {0}).</param>
        /// <returns>A disposable object that can be used to close the group.</returns>
        public static IDisposable OpenGroup( this IActivityMonitor @this, LogLevel level, Exception ex, string format, object arg0 )
        {
            return FilteredGroup( @this, level & LogLevel.Mask ) ?? @this.UnfilteredOpenGroup( ActivityMonitor.EmptyTag, level | LogLevel.IsFiltered, null, String.Format( format, arg0 ), DateTime.UtcNow, ex );
        }

        /// <summary>
        /// Opens a log level associated to an <see cref="Exception"/>. <see cref="IActivityMonitor.CloseGroup">CloseGroup</see> must be called in order to
        /// close the group, or the returned object must be disposed.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="level">Log level. Since we are opening a group, the current <see cref="IActivityMonitor.ActualFilter">Filter</see> is ignored.</param>
        /// <param name="ex">Exception to log.</param>
        /// <param name="format">Text format for group title.</param>
        /// <param name="arg0">First parameter to format (placeholder {0}).</param>
        /// <param name="arg1">Second parameter to format (placeholder {1}).</param>
        /// <returns>A disposable object that can be used to close the group.</returns>
        public static IDisposable OpenGroup( this IActivityMonitor @this, LogLevel level, Exception ex, string format, object arg0, object arg1 )
        {
            return FilteredGroup( @this, level & LogLevel.Mask ) ?? @this.UnfilteredOpenGroup( ActivityMonitor.EmptyTag, level | LogLevel.IsFiltered, null, String.Format( format, arg0, arg1 ), DateTime.UtcNow, ex );
        }

        /// <summary>
        /// Opens a log level associated to an <see cref="Exception"/>. <see cref="IActivityMonitor.CloseGroup">CloseGroup</see> must be called in order to
        /// close the group, or the returned object must be disposed.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="level">Log level. Since we are opening a group, the current <see cref="IActivityMonitor.ActualFilter">Filter</see> is ignored.</param>
        /// <param name="ex">Exception to log.</param>
        /// <param name="format">Text format for group title.</param>
        /// <param name="arg0">First parameter to format (placeholder {0}).</param>
        /// <param name="arg1">Second parameter to format (placeholder {1}).</param>
        /// <param name="arg2">Third parameter to format (placeholder {2}).</param>
        /// <returns>A disposable object that can be used to close the group.</returns>
        public static IDisposable OpenGroup( this IActivityMonitor @this, LogLevel level, Exception ex, string format, object arg0, object arg1, object arg2 )
        {
            return FilteredGroup( @this, level & LogLevel.Mask ) ?? @this.UnfilteredOpenGroup( ActivityMonitor.EmptyTag, level | LogLevel.IsFiltered, null, String.Format( format, arg0, arg1, arg2 ), DateTime.UtcNow, ex );
        }

        /// <summary>
        /// Opens a log level associated to an <see cref="Exception"/>. <see cref="IActivityMonitor.CloseGroup">CloseGroup</see> must be called in order to
        /// close the group, or the returned object must be disposed.
        /// </summary>
        /// <param name="this">This <see cref="IActivityMonitor"/> object.</param>
        /// <param name="level">Log level. Since we are opening a group, the current <see cref="IActivityMonitor.ActualFilter">Filter</see> is ignored.</param>
        /// <param name="ex">Exception to log.</param>
        /// <param name="format">A composite format for the group title.</param>
        /// <param name="arguments">Arguments to format.</param>
        /// <returns>A disposable object that can be used to close the group.</returns>
        public static IDisposable OpenGroup( this IActivityMonitor @this, LogLevel level, Exception ex, string format, params object[] arguments )
        {
            return FilteredGroup( @this, level & LogLevel.Mask ) ?? @this.UnfilteredOpenGroup( ActivityMonitor.EmptyTag, level | LogLevel.IsFiltered, null, String.Format( format, arguments ), DateTime.UtcNow, ex );
        }

        #endregion

        #endregion
    }
}
