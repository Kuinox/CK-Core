#region LGPL License
/*----------------------------------------------------------------------------
* This file (CK.Core\ActivityLogger\Impl\DefaultActivityLogger.cs) is part of CiviKey. 
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
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace CK.Core
{
    /// <summary>
    /// Simple implementation of <see cref="IDefaultActivityLogger"/> with a <see cref="Tap"/> to register <see cref="IActivityLoggerSink"/>, 
    /// an <see cref="ErrorCounter"/> and a <see cref="PathCatcher"/>.
    /// </summary>
    public class DefaultActivityLogger : ActivityLogger, IDefaultActivityLogger
    {
        [ExcludeFromCodeCoverage]
        class EmptyDefault : ActivityLoggerEmpty, IDefaultActivityLogger
        {
            public ActivityLoggerTap Tap
            {
                get { return ActivityLoggerTap.Empty; }
            }

            public ActivityLoggerErrorCounter ErrorCounter
            {
                get { return ActivityLoggerErrorCounter.Empty; }
            }

            public ActivityLoggerPathCatcher PathCatcher
            {
                get { return ActivityLoggerPathCatcher.Empty; }
            }

        }

        /// <summary>
        /// Empty <see cref="IDefaultActivityLogger"/> (null object design pattern).
        /// </summary>
        static public readonly IDefaultActivityLogger Empty = new EmptyDefault();

        readonly ActivityLoggerTap _tap;
        readonly ActivityLoggerErrorCounter _errorCounter;
        readonly ActivityLoggerPathCatcher _pathCatcher;

        public DefaultActivityLogger( bool generateErrorCounterConlusion = true )
        {
            // Order does not really matter matters here thanks to Closing/Closed pattern, but
            // we order them in the "logical" sense.

            // Will be the last one as beeing called: it is the final sink.
            _tap = new ActivityLoggerTap( this );
            // Will be called AFTER the ErrorCounter.
            _pathCatcher = new ActivityLoggerPathCatcher( this );
            // Will be called first.
            _errorCounter = new ActivityLoggerErrorCounter( this, generateErrorCounterConlusion );
        }

        ActivityLoggerTap IDefaultActivityLogger.Tap 
        { 
            get { return _tap; } 
        }

        ActivityLoggerErrorCounter IDefaultActivityLogger.ErrorCounter
        {
            get { return _errorCounter; }
        }

        ActivityLoggerPathCatcher IDefaultActivityLogger.PathCatcher
        {
            get { return _pathCatcher; }
        }

    }
}
