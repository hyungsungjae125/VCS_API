using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace VCSdotnet{
    public class Database
    {
        string connStr="Server=tcp:vcsserver.database.windows.net,1433;Initial Catalog=VCS_DB;Persist Security Info=False;User ID=hsj;Password=wo348191@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        SqlConnection conn;
        bool status;
        public Database(){
            status=GetConnection();
        }
        
        private bool GetConnection(){
            try{
                conn= new SqlConnection(connStr);
                conn.Open();
                Console.WriteLine("-----------sucess!!----------------");
                return true;
            }catch{
                conn.Close();
                conn=null;
                Console.WriteLine("-----------fail...----------------");
                return false;
            }
        }
        public void Close(){
            conn.Close();
        }

        public ArrayList GetList(string sql){
            ArrayList list = new ArrayList();
            if(status){
                try{
                    SqlCommand comm = new SqlCommand();
                    comm.Connection=conn;
                    comm.CommandText=sql;
                    comm.CommandType=CommandType.StoredProcedure;
                    
                    SqlDataReader reader = comm.ExecuteReader();
                   
                    while(reader.Read()){
                        string[] arr = new string[reader.FieldCount];
                        for(int i=0;i<reader.FieldCount;i++){
                            arr[i] = reader.GetValue(i).ToString();
                        }
                        list.Add(arr);
                        // Hashtable ht = new Hashtable();
                        // for(int i=0;i<reader.FieldCount;i++){
                        //     ht.Add(reader.GetName(i).ToString(),reader.GetValue(i).ToString());
                        // }
                        // list.Add(ht);
                    }
                    reader.Close();
                    return list;
                }catch{
                    return list;
                }
            }
            return list;
        }

        public ArrayList GetList(string sql,Hashtable ht){
            if(status){
                ArrayList result = new ArrayList();
                try{
                    SqlCommand comm = new SqlCommand();
                    comm.Connection=conn;
                    comm.CommandText=sql;
                    comm.CommandType=CommandType.StoredProcedure;
                    foreach(DictionaryEntry data in ht){
                        comm.Parameters.AddWithValue(data.Key.ToString(),data.Value);
                    }
                    SqlDataReader reader = comm.ExecuteReader();
                    while(reader.Read()){
                        Hashtable col = new Hashtable();
                        for(int i=0;i<reader.FieldCount;i++){
                            col.Add(reader.GetName(i),reader.GetValue(i));
                        }
                        result.Add(col);
                    }
                    reader.Close();
                    return result;
                }catch{
                    return null;
                }
            }
            return null;
        }

        // public bool NonQuery(string sql,Hashtable ht){
        //     if(status){
        //         try{
        //             SqlCommand comm = new SqlCommand();
        //             comm.Connection=conn;
        //             comm.CommandText=sql;
        //             comm.CommandType=CommandType.StoredProcedure;
        //             foreach(DictionaryEntry data in ht){
        //                 comm.Parameters.AddWithValue(data.Key.ToString(),data.Value);
        //             }

        //             int cnt = comm.ExecuteNonQuery();
        //             Console.WriteLine("------------------>>>>>>>>"+cnt);
        //             return true;
        //         }catch{
        //             return false;
        //         }
        //     }
        //     return false;
        // }

        public int NonQuery(string sql,Hashtable ht){
            if(status){
                try{
                    SqlCommand comm = new SqlCommand();
                    comm.Connection=conn;
                    comm.CommandText=sql;
                    comm.CommandType=CommandType.StoredProcedure;
                    foreach(DictionaryEntry data in ht){
                        comm.Parameters.AddWithValue(data.Key.ToString(),data.Value);
                    }

                    int cnt = comm.ExecuteNonQuery();
                    Console.WriteLine("------------------>>>>>>>>"+cnt);
                    return cnt;
                }catch{
                    return 0;
                }
            }
            return 0;
        }
    }
}