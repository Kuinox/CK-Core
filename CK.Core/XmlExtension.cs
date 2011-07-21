#region LGPL License
/*----------------------------------------------------------------------------
* This file (CK.Core\XmlExtension.cs) is part of CiviKey. 
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
* Copyright © 2007-2010, 
*     Invenietis <http://www.invenietis.com>,
*     In’Tech INFO <http://www.intechinfo.fr>,
* All rights reserved. 
*-----------------------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace CK.Core
{
    /// <summary>
    /// Extension methods for <see cref="XmlReader"/> and <see cref="XmlWriter"/>
    /// classes.
    /// </summary>
    public static class XmlExtension
    {
        /// <summary>
        /// Little helper that only increases source code readability: it calls <see cref="XmlReader.ReadEndElement"/>
        /// that checks the name of the closing element. This "helper" forces the developper to explicitely
        /// write this name.
        /// </summary>
        /// <param name="r">This <see cref="XmlReader"/>.</param>
        /// <param name="name">Name of the closing element.</param>
        static public void ReadEndElement( this XmlReader r, string name )
        {
            if( r.NodeType != XmlNodeType.EndElement || r.Name != name )
            {
                throw new XmlException( String.Format( R.ExpectedXmlEndElement, name ) );
            }
            r.ReadEndElement();
        }

        /// <summary>
        /// Gets a boolean attribute by name.
        /// </summary>
        /// <param name="r">This <see cref="XmlReader"/>.</param>
        /// <param name="name">Name of the attribute.</param>
        /// <param name="defaultValue">Default value if the attribute does not exist.</param>
        static public bool GetAttributeBoolean( this XmlReader r, string name, bool defaultValue )
        {
            string s = r.GetAttribute( name );
            return s != null ? XmlConvert.ToBoolean( s ) : defaultValue;
        }

        /// <summary>
        /// Gets a <see cref="DateTime"/> attribute by name. It uses <see cref="XmlDateTimeSerializationMode.RoundtripKind"/>.
        /// </summary>
        /// <param name="r">This <see cref="XmlReader"/>.</param>
        /// <param name="name">Name of the attribute.</param>
        /// <param name="defaultValue">Default value if the attribute does not exist.</param>
        static public DateTime GetAttributeDateTime( this XmlReader r, string name, DateTime defaultValue )
        {
            string s = r.GetAttribute( name );
            return s != null ? XmlConvert.ToDateTime( s, XmlDateTimeSerializationMode.RoundtripKind ) : defaultValue;
        }

        /// <summary>
        /// Gets a <see cref="Version"/> attribute by name.
        /// </summary>
        /// <param name="r">This <see cref="XmlReader"/>.</param>
        /// <param name="name">Name of the attribute.</param>
        /// <param name="defaultValue">Default value if the attribute does not exist.</param>
        static public Version GetAttributeVersion( this XmlReader r, string name, Version defaultValue )
        {
            string s = r.GetAttribute( name );
            return s != null ? new Version( s ) : defaultValue;
        }

        /// <summary>
        /// Gets an <see cref="Int32"/> attribute by name.
        /// </summary>
        /// <param name="r">This <see cref="XmlReader"/>.</param>
        /// <param name="name">Name of the attribute.</param>
        /// <param name="defaultValue">Default value if the attribute does not exist.</param>
        static public int GetAttributeInt( this XmlReader r, string name, int defaultValue )
        {
            string s = r.GetAttribute( name );
            int i;
            if( s != null && int.TryParse( s, out i ) ) return i;
            return defaultValue;
        }

        /// <summary>
        /// Gets an enum value.
        /// </summary>
        /// <typeparam name="T">Type of the enum. There is no way (in c#) to constraint the type to Enum - nor to Delegate, this is why 
        /// the constraint restricts only the type to be a value type.</typeparam>
        /// <param name="r">This <see cref="XmlReader"/>.</param>
        /// <param name="name">Name of the attribute.</param>
        /// <param name="defaultValue">Default value if the attribute does not exist or can not be parsed.</param>
        /// <returns>The parsed value or the default value.</returns>
        static public T GetAttributeEnum<T>( this XmlReader r, string name, T defaultValue ) where T : struct
        {
            T result;
            string s = r.GetAttribute( name );
            if( s == null || !Enum.TryParse( s, out result ) ) result = defaultValue;
            return result;
        }


    }
}
