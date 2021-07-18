using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Priority_Express_Service.helper
{
    /// <summary>
    /// Converts a DataTable to a list with generic objects
    /// </summary>
    /// <typeparam name="T">Generic object</typeparam>
    /// <param name="table">DataTable</param>
    /// <returns>List with generic objects</returns>

    public static class Helper
    {
        public static List<T> DataTableToClass<T>(this DataTable table) where T : class, new()
        {
            try
            {
                List<T> list = new List<T>();
                Type businessEntityType = typeof(T);
                List<T> entitys = new List<T>();
                Hashtable hashtable = new Hashtable();
                PropertyInfo[] properties = businessEntityType.GetProperties();
                foreach (PropertyInfo info in properties)
                {
                    hashtable[info.Name.ToUpper()] = info;
                }
                foreach (var row in table.AsEnumerable())
                {
                    T obj = new T();

                    for ( int fieldCount=0; fieldCount < table.Columns.Count; fieldCount++)
                    {
                        try
                        {
                            PropertyInfo propertyInfo =
                                (PropertyInfo) hashtable[table.Columns[fieldCount].ColumnName.ToUpper()]; //obj.GetType().GetProperty(prop.Name);

                            if(propertyInfo==null) continue;
                            var type = Nullable.GetUnderlyingType(propertyInfo.PropertyType)==null ? propertyInfo.PropertyType : Nullable.GetUnderlyingType(propertyInfo.PropertyType);
                            if (row[fieldCount] != DBNull.Value)
                            propertyInfo.SetValue(obj, Convert.ChangeType(row[fieldCount],type ), null);
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return null;
            }
        }

    }
}
