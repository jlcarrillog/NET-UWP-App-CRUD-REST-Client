using App.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace App.Data
{
    class EmpleadosDataService
    {
        private static HttpClient client = new HttpClient();

        public EmpleadosDataService()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://unitecti-api.azurewebsites.net/api/Empleados/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<List<Empleado>> ToList()
        {
            var data = new List<Empleado>();
            try
            {
                var response = await client.GetAsync("");
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<List<Empleado>>(responseString);
                }
                client.Dispose();
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        internal async Task<Empleado> Find(Guid id)
        {
            var data = new Empleado();
            try
            {
                var response = await client.GetAsync(id.ToString());
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<Empleado>(responseString);
                }
                client.Dispose();
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        internal async Task Add(Empleado data)
        {
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            var response = await client.PostAsync("", content);

            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new HttpRequestException();
            }
            client.Dispose();
        }
        internal async Task Update(Empleado data)
        {
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            var response = await client.PutAsync(data.EmpleadoID.ToString(), content);

            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new HttpRequestException();
            }
            client.Dispose();
        }
        internal async Task Remove(Guid id)
        {
            var response = await client.DeleteAsync(id.ToString());

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
            }
            client.Dispose();
        }
    }
}
