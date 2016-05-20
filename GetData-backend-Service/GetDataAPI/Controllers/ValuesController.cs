using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GetDataAPI.Controllers
{
   
    public class ValuesController : ApiController
    {
        // GET api/values
        //This function is called by the Mobile app to display data related to the devices and get status of each farm area
        public List<string> Get()
        {
            SqlConnectionStringBuilder csBuilder;
            csBuilder = new SqlConnectionStringBuilder("<Connection string to the database containing information about the field indicators");
            SqlConnection connection = new SqlConnection(csBuilder.ToString());
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = @"<select query for reading huidity, status, farm, plot IDs>";
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            
            List<string> data = new List<string>();
            while (reader.Read())
            {
                //read fields here
                //reader[0]
                //reader[1]
                //reader[2]
                //reader[3]

                //    data.Add(<above fields>);
            }

            connection.Close();
            return data;
            
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
