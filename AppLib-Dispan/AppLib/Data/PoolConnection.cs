using System;
using System.Collections.Generic;
using System.Text;

namespace AppLib.Data
{
    public class PoolConnection
    {
        private List<Connection> Items = new List<Connection>();

        public Boolean Exist(String ConnectionName)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].Name.ToUpper().Equals(ConnectionName.ToUpper()))
                {
                    return true;
                }
            }

            return false;
        }

        public Connection Get(int Index)
        {
            return Items[Index];
        }

        public Connection Get(String ConnectionName)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].Name.ToUpper().Equals(ConnectionName.ToUpper()))
                {
                    return Items[i];
                }
            }

            return null;
        }

        public Connection Get()
        {
            return this.Get("Start");
        }

        public int? Index(String ConnectionName)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].Name.ToUpper().Equals(ConnectionName.ToUpper()))
                {
                    return i;
                }
            }

            return null;
        }

        public Boolean Add(String name, Global.Types.Database database, String connectionString)
        {
            try
            {
                if (!this.Exist(name))
                {
                    Connection conn = new Connection(name, database, connectionString);
                    Items.Add(conn);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public Boolean Remove(String ConnectionName)
        {
            try
            {
                while (this.Exist(ConnectionName))
                {
                    Items.RemoveAt((int)this.Index(ConnectionName));
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public int Size()
        {
            return Items.Count;
        }

    }
}
