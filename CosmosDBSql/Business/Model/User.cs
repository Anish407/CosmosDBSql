using Newtonsoft.Json;
using System;

namespace CosmosDBSql.Business.Model
{

    public class Rootobject
    {
        public Result[] results { get; set; }
    }

    public class Result
    {
        public string gender { get; set; }
        public Name name { get; set; }
        public string email { get; set; }
        public Dob dob { get; set; }
        public Id id { get; set; }

    }

    public class Name
    {
        public string title { get; set; }
        public string first { get; set; }
        public string last { get; set; }
    }

    public class Dob
    {
        public DateTime date { get; set; }
        public int age { get; set; }
    }

    public class Registered
    {
        public DateTime date { get; set; }
        public int age { get; set; }
    }

    public class Id
    {
        public string name { get; set; }
        public string value { get; set; }
    }


    public class User
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }


}
