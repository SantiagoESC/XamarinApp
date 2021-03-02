using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TestAPP.Models;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System.Net;
using System.IO;
 
namespace TestAPP.Services
{
	public class ApiService
	{
		public async Task<Response> CheckConnection()
		{
			if (!CrossConnectivity.Current.IsConnected)
			{
				return new Response
				{
					IsSuccess = false,
					Message = "Please turn on your internet settings.",
				};
			}

			var isReachable = await CrossConnectivity.Current.IsRemoteReachable(
				"google.com");
			if (!isReachable)
			{
				return new Response
				{
					IsSuccess = false,
					Message = "Check you internet connection.",
				};
			}

			return new Response
			{
				IsSuccess = true,
				Message = "Ok",
			};
		}

		/* public async Task<TokenResponse> GetToken(
			 string urlBase,
			 string username,
			 string password)
		 {
			 try
			 {
				 var client = new HttpClient();
				 client.BaseAddress = new Uri(urlBase);
				 var response = await client.PostAsync("Token",
					 new StringContent(string.Format(
					 "grant_type=password&username={0}&password={1}",
					 username, password),
					 Encoding.UTF8, "application/x-www-form-urlencoded"));
				 var resultJSON = await response.Content.ReadAsStringAsync();
				 var result = JsonConvert.DeserializeObject<TokenResponse>(
					 resultJSON);
				 return result;
			 }
			 catch
			 {
				 return null;
			 }
		 }*/

		public async Task<Response> Get<T>(
			string urlBase,
			string servicePrefix,
			string controller,
			string tokenType,
			string accessToken,
			int id)
		{
			try
			{
				var client = new HttpClient();
				client.DefaultRequestHeaders.Authorization =
					new AuthenticationHeaderValue(tokenType, accessToken);
				client.BaseAddress = new Uri(urlBase);
				var url = string.Format(
					"{0}{1}/{2}",
					servicePrefix,
					controller,
					id);
				var response = await client.GetAsync(url);

				if (!response.IsSuccessStatusCode)
				{
					return new Response
					{
						IsSuccess = false,
						Message = response.StatusCode.ToString(),
					};
				}

				var result = await response.Content.ReadAsStringAsync();
				var model = JsonConvert.DeserializeObject<T>(result);
				return new Response
				{
					IsSuccess = true,
					Message = "Ok",
					Result = model,
				};
			}
			catch (Exception ex)
			{
				return new Response
				{
					IsSuccess = false,
					Message = ex.Message,
				};
			}
		}

		public async Task<Response> GetList<T>(string urlBase, string servicePrefix, string controller)
		{
			try
			{
				var client = new HttpClient();
				client.BaseAddress = new Uri(urlBase);
				var url = string.Format("{0}{1}", servicePrefix, controller);
				var response = await client.GetAsync(url);
				var result = await response.Content.ReadAsStringAsync();

				if (!response.IsSuccessStatusCode)
				{
					return new Response
					{
						IsSuccess = false,
						Message = result,
					};
				}

				var list = JsonConvert.DeserializeObject<List<T>>(result);
				return new Response
				{
					IsSuccess = true,
					Message = "Ok",
					Result = list,
				};
			}
			catch (Exception ex)
			{
				return new Response
				{
					IsSuccess = false,
					Message = ex.Message,
				};
			}
		}

		public string MakeRequest(string httpMethod, string url)
		{
			var request = (HttpWebRequest)WebRequest.Create(url);
			request.Method = httpMethod;
			request.Accept = "application/json";

			try
			{
				using (System.Net.WebResponse response = request.GetResponse())
				{
					using(Stream strReader = response.GetResponseStream())
					{
						using(StreamReader objReader = new StreamReader(strReader))
						{
							var a= JsonFormatter(objReader.ReadToEnd());
							return objReader.ReadToEnd();
						}
					}
					//return (HttpWebResponse)response;

				}
			}
			catch (WebException ex)
			{
				Console.WriteLine(ex);
			}
			return null;
		}

		public static string JsonFormatter(string json)
		{
			StringBuilder builder = new StringBuilder();

			bool quotes = false;

			bool ignore = false;

			int offset = 0;

			int position = 0;

			if (string.IsNullOrEmpty(json))
			{
				return string.Empty;
			}

			json = json.Replace(Environment.NewLine, "").Replace("\t", "");

			foreach (char character in json)
			{
				switch (character)
				{
					case '"':
						if (!ignore)
						{
							quotes = !quotes;
						}
						break;
					case '\'':
						if (quotes)
						{
							ignore = !ignore;
						}
						break;
				}

				if (quotes)
				{
					builder.Append(character);
				}
				else
				{
					switch (character)
					{
						case '{':
						case '[':
							builder.Append(character);
							builder.Append(Environment.NewLine);
							builder.Append(new string(' ', ++offset * 4));
							break;
						case '}':
						case ']':
							builder.Append(Environment.NewLine);
							builder.Append(new string(' ', --offset * 4));
							builder.Append(character);
							break;
						case ',':
							builder.Append(character);
							builder.Append(Environment.NewLine);
							builder.Append(new string(' ', offset * 4));
							break;
						case ':':
							builder.Append(character);
							builder.Append(' ');
							break;
						default:
							if (character != ' ')
							{
								builder.Append(character);
							}
							break;
					}

					position++;
				}
			}

			return builder.ToString().Trim();
		}

		public List<T> GetList<T>(string json)
		{
			var list = JsonConvert.DeserializeObject<List<T>>(json);

			return list;

		}



		public async Task<Response> GetList<T>(
			string urlBase,
			string servicePrefix,
			string controller,
			string tokenType,
			string accessToken)
		{
			try
			{
				var client = new HttpClient();
				client.DefaultRequestHeaders.Authorization =
					new AuthenticationHeaderValue(tokenType, accessToken);
				client.BaseAddress = new Uri(urlBase);
				var url = string.Format("{0}{1}", servicePrefix, controller);
				var response = await client.GetAsync(url);
				var result = await response.Content.ReadAsStringAsync();

				if (!response.IsSuccessStatusCode)
				{
					return new Response
					{
						IsSuccess = false,
						Message = result,
					};
				}

				var list = JsonConvert.DeserializeObject<List<T>>(result);
				return new Response
				{
					IsSuccess = true,
					Message = "Ok",
					Result = list,
				};
			}
			catch (Exception ex)
			{
				return new Response
				{
					IsSuccess = false,
					Message = ex.Message,
				};
			}
		}

		public async Task<Response> GetList<T>(
			string urlBase,
			string servicePrefix,
			string controller,
			string tokenType,
			string accessToken,
			int id)
		{
			try
			{
				var client = new HttpClient();
				client.DefaultRequestHeaders.Authorization =
					new AuthenticationHeaderValue(tokenType, accessToken);
				client.BaseAddress = new Uri(urlBase);
				var url = string.Format(
					"{0}{1}/{2}",
					servicePrefix,
					controller,
					id);
				var response = await client.GetAsync(url);

				if (!response.IsSuccessStatusCode)
				{
					return new Response
					{
						IsSuccess = false,
						Message = response.StatusCode.ToString(),
					};
				}

				var result = await response.Content.ReadAsStringAsync();
				var list = JsonConvert.DeserializeObject<List<T>>(result);
				return new Response
				{
					IsSuccess = true,
					Message = "Ok",
					Result = list,
				};
			}
			catch (Exception ex)
			{
				return new Response
				{
					IsSuccess = false,
					Message = ex.Message,
				};
			}
		}

		public async Task<Response> Post<T>(
			string urlBase,
			string servicePrefix,
			string controller,
			string tokenType,
			string accessToken,
			T model)
		{
			try
			{
				var request = JsonConvert.SerializeObject(model);
				var content = new StringContent(
					request, Encoding.UTF8,
					"application/json");
				var client = new HttpClient();
				client.DefaultRequestHeaders.Authorization =
					new AuthenticationHeaderValue(tokenType, accessToken);
				client.BaseAddress = new Uri(urlBase);
				var url = string.Format("{0}{1}", servicePrefix, controller);
				var response = await client.PostAsync(url, content);
				var result = await response.Content.ReadAsStringAsync();

				if (!response.IsSuccessStatusCode)
				{
					var error = JsonConvert.DeserializeObject<Response>(result);
					error.IsSuccess = false;
					return error;
				}

				var newRecord = JsonConvert.DeserializeObject<T>(result);

				return new Response
				{
					IsSuccess = true,
					Message = "Record added OK",
					Result = newRecord,
				};
			}
			catch (Exception ex)
			{
				return new Response
				{
					IsSuccess = false,
					Message = ex.Message,
				};
			}
		}

		public async Task<Response> Post<T>(
			string urlBase,
			string servicePrefix,
			string controller,
			T model)
		{
			try
			{
				var request = JsonConvert.SerializeObject(model);
				var content = new StringContent(
					request,
					Encoding.UTF8,
					"application/json");
				var client = new HttpClient();
				client.BaseAddress = new Uri(urlBase);
				var url = string.Format("{0}{1}", servicePrefix, controller);
				var response = await client.PostAsync(url, content);

				if (!response.IsSuccessStatusCode)
				{
					return new Response
					{
						IsSuccess = false,
						Message = response.StatusCode.ToString(),
					};
				}

				var result = await response.Content.ReadAsStringAsync();
				var newRecord = JsonConvert.DeserializeObject<T>(result);

				return new Response
				{
					IsSuccess = true,
					Message = "Record added OK",
					Result = newRecord,
				};
			}
			catch (Exception ex)
			{
				return new Response
				{
					IsSuccess = false,
					Message = ex.Message,
				};
			}
		}

		public async Task<Response> Put<T>(
			string urlBase,
			string servicePrefix,
			string controller,
			string tokenType,
			string accessToken,
			T model)
		{
			try
			{
				var request = JsonConvert.SerializeObject(model);
				var content = new StringContent(
					request,
					Encoding.UTF8, "application/json");
				var client = new HttpClient();
				client.DefaultRequestHeaders.Authorization =
					new AuthenticationHeaderValue(tokenType, accessToken);
				client.BaseAddress = new Uri(urlBase);
				var url = string.Format(
					"{0}{1}/{2}",
					servicePrefix,
					controller,
					model.GetHashCode());
				var response = await client.PutAsync(url, content);
				var result = await response.Content.ReadAsStringAsync();

				if (!response.IsSuccessStatusCode)
				{
					var error = JsonConvert.DeserializeObject<Response>(result);
					error.IsSuccess = false;
					return error;
				}

				var newRecord = JsonConvert.DeserializeObject<T>(result);

				return new Response
				{
					IsSuccess = true,
					Result = newRecord,
				};
			}
			catch (Exception ex)
			{
				return new Response
				{
					IsSuccess = false,
					Message = ex.Message,
				};
			}
		}

		public async Task<Response> Delete<T>(
			string urlBase,
			string servicePrefix,
			string controller,
			string tokenType,
			string accessToken,
			T model)
		{
			try
			{
				var client = new HttpClient();
				client.BaseAddress = new Uri(urlBase);
				client.DefaultRequestHeaders.Authorization =
					new AuthenticationHeaderValue(tokenType, accessToken);
				var url = string.Format(
					"{0}{1}/{2}",
					servicePrefix,
					controller,
					model.GetHashCode());
				var response = await client.DeleteAsync(url);
				var result = await response.Content.ReadAsStringAsync();

				if (!response.IsSuccessStatusCode)
				{
					var error = JsonConvert.DeserializeObject<Response>(result);
					error.IsSuccess = false;
					return error;
				}

				return new Response
				{
					IsSuccess = true,
				};
			}
			catch (Exception ex)
			{
				return new Response
				{
					IsSuccess = false,
					Message = ex.Message,
				};
			}
		}
	}
}
