using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using KarloKorbarZadatak.Validators;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KarloKorbarZadatak.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        // GET: api/<PersonController>
        [HttpGet]
        public IEnumerable<Person> Get()
        {

            IEnumerable<Person> persons = new List<Person>();
            var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
            };

            string path = System.IO.File.ReadAllText("./CONFIG/CSVpathConf.txt"); //obicno bi stavio da je config file json ili xml ali posto treba samo spremati jedan path txt je fine

            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                persons = csv.GetRecords<Person>().ToList();
            }
            return persons;
        }

        // GET api/<PersonController>/5
        [HttpGet("{id}")]
        public Person Get(int id)
        {
            return new Person { Ime = "Pero", Prezime = "Peric", Grad = "Zagreb", PostanskiBr = "1000", Telefon = "09112334567" };
        }

        // POST api/<PersonController>
        [HttpPost]
        public void Post([FromBody] List<Person> value)
        {
            foreach(Person person in value) 
            {
                if (ValidatorFactory.PersonValidator().IsValid(person))
                {
                    try
                    {
                        //add my man to the database
                        using (SqlConnection conn = new SqlConnection("Server=DESKTOP-V4ANS0V;Database=DBname;Trusted_Connection=True;"))
                        {
                            conn.Open();
                            SqlCommand cmd = new SqlCommand("dbo.AddPerson", conn);
                            cmd.CommandType = CommandType.StoredProcedure;
                            
                            cmd.Parameters.Add(new SqlParameter("@Ime", person.Ime));
                            cmd.Parameters.Add(new SqlParameter("@Prezime", person.Prezime));
                            cmd.Parameters.Add(new SqlParameter("@PostanskiBr", person.PostanskiBr));
                            cmd.Parameters.Add(new SqlParameter("@Grad", person.Grad));
                            cmd.Parameters.Add(new SqlParameter("@Telefon", person.Telefon));   
                                
                            cmd.ExecuteReader();
                        }
                    }
                    catch (Exception)
                    {
                        //u slucaju duplih podataka ce sql baciti exception, koji se handla ovdje
                        throw;
                    }
                }
            }
        }

        // PUT api/<PersonController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Person value)
        {
        }

        // DELETE api/<PersonController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
