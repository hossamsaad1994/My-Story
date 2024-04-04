using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace MyStore.Pages.Clients
{
    public class CreateModel : PageModel
    {
        public ClientInfo clientinfo = new ClientInfo();
        public string errormessage = "";
        public string successmessage = "";
		public void OnGet()
        {
        }
        public void OnPost()
        {
            clientinfo.Name = Request.Form["Name"];
            clientinfo.Email = Request.Form["Email"];
            clientinfo.Phone = Request.Form["Phone"];
			clientinfo.Address = Request.Form["Address"];

			if (clientinfo.Name.Length == 0 || clientinfo.Email.Length == 0 ||
				clientinfo.Phone.Length == 0 || clientinfo.Address.Length == 0)
            {
                errormessage = "All Fields Are Required";
                return;
            }

            try
            {

				string connectionString = "Data Source=HOSSAM-PC;Initial Catalog=MyStore;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
				using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = " INSERT INTO CLIENTS" +
                        "(NAME, EMAIL, PHONE, ADDRESS) VALUES " +
                        "(@NAME, @EMAIL, @PHONE, @ADDRESS);";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@Name", clientinfo.Name);
                        command.Parameters.AddWithValue ("@EMAIL", clientinfo.Email);
						command.Parameters.AddWithValue("@PHONE", clientinfo.Phone);
						command.Parameters.AddWithValue("@ADDRESS", clientinfo.Address);

                        command.ExecuteNonQuery();
					}

				}

			}
            catch (Exception ex)
            {
                errormessage = ex.Message;
                return;
            }

            clientinfo.Name = "";
            clientinfo.Email = "";
            clientinfo.Phone = "";
            clientinfo.Address = "";
            successmessage = "New Client Added Correctly";
            Response.Redirect("/Clients/Index");
			

        }
    }

}
