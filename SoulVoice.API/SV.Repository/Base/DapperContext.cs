using System;
using System.Data;
using DapperExtensions.Sql;
using MySql.Data.MySqlClient;
using SV.Common.Options;

namespace SV.Repository.Base
{
    public class DapperContext :IDisposable
    {
        internal IDbConnection Conn { get; set; }
        
        public DapperContext()
        {
            if (string.IsNullOrEmpty(DbContextOption.QueryString))
                throw new ArgumentNullException(nameof(DbContextOption.QueryString));
            DapperExtensions.DapperExtensions.SqlDialect = new MySqlDialect();
            this.Conn = new MySqlConnection(DbContextOption.QueryString);
        }
        

        public IDbConnection GetConnection()
        {
            var conn = new MySqlConnection(DbContextOption.QueryString);
            conn.Open();
            return conn;
        }


        public IDbConnection GetConnection(string strConn)
        {
            var conn = new MySqlConnection(DbContextOption.QueryString);
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
                var conn = GetConnection(DbContextOption.QueryString);
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

        void IDisposable.Dispose()
        {
            Conn.Dispose();
        }


    }
}