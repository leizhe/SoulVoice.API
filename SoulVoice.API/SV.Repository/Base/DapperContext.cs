using System;
using System.Data;
using MySql.Data.MySqlClient;
using SV.Common.Options;

namespace SV.Repository.Base
{
    public class DapperContext 
    {
        private DbContextOption _option;

        //private readonly string _connstr = Global.QueryDB;
        //public DapperContext()
        //{
        //}

        //public DapperContext(string connstr)
        //{

        //    _connstr = connstr;
        //}
        public DapperContext(DbContextOption option)
        {
            if (option == null)
                throw new ArgumentNullException(nameof(option));
            if (string.IsNullOrEmpty(option.QueryString))
                throw new ArgumentNullException(nameof(option.QueryString));
            _option = option;
        }
        public IDbConnection GetConnection()
        {
            var conn = new MySqlConnection(_option.QueryString);
            conn.Open();
            return conn;
        }


        public IDbConnection GetConnection(string strConn)
        {
            var conn = new MySqlConnection(strConn);
            conn.Open();
            return conn;
        }


        public Tuple<bool, string> TestConn(string connstring)
        {
            bool isopen = false;
            string msg = string.Empty;

            try
            {
                var conn = GetConnection(connstring);
                if (conn.State == ConnectionState.Open)
                {
                    isopen = true;
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }


            return new Tuple<bool, string>(isopen, msg);
        }
        public Tuple<bool, string> TestConn()
        {
            bool isopen = false;
            string msg = string.Empty;

            try
            {
                var conn = GetConnection(_option.QueryString);
                if (conn.State == ConnectionState.Open)
                {
                    isopen = true;
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }


            return new Tuple<bool, string>(isopen, msg);
        }


    }
}