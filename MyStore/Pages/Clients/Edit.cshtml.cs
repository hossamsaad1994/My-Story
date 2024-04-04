using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace MyStore.Pages.Clients
{
	public class EditModel : PageModel
	{
		public ClientInfo clientinfo = new ClientInfo();
		public string errormessage = "";
		public string successmessage = "";

		public void OnGet()
		{
			string id = Request.Query["id"];

			try
			{
				string connectionString = "Data Source=HOSSAM-PC;Initial Catalog=MyStore;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					string sql = "SELECT * FROM Clients WHERE id=@id";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@id", id);
						using (SqlDataReader reader = command.ExecuteReader())
						{
							if (reader.Read())
							{
								clientinfo.ID = "" + reader.GetInt32(0);
								clientinfo.Name = reader.GetString(1);
								clientinfo.Email = reader.GetString(2);
								clientinfo.Phone = reader.GetString(3);
								clientinfo.Address = reader.GetString(4);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				errormessage = ex.Message;
			}
		}

		public void OnPost()
		{
			clientinfo.ID = Request.Form["ID"];
			clientinfo.Name = Request.Form["Name"];
			clientinfo.Email = Request.Form["Email"];
			clientinfo.Phone = Request.Form["Phone"];
			clientinfo.Address = Request.Form["Address"];

			if (clientinfo.ID.Length == 0 || clientinfo.Name.Length == 0 || clientinfo.Email.Length == 0 ||
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
					string sql = "UPDATE Clients SET NAME = @Name, Address = @Address, Phone = @Phone, Email = @Email WHERE id = @id";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@Name", clientinfo.Name);
						command.Parameters.AddWithValue("@Email", clientinfo.Email);
						command.Parameters.AddWithValue("@Phone", clientinfo.Phone);
						command.Parameters.AddWithValue("@Address", clientinfo.Address);
						command.Parameters.AddWithValue("@id", clientinfo.ID);

						command.ExecuteNonQuery();
					}
				}
			}
			catch (Exception ex)
			{
				errormessage = ex.Message;
				return;
			}

			Response.Redirect("/Clients/Index");
		}
	}
}
