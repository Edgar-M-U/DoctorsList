using DoctorsList.Abstractions;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorsList.MVVM.MODEL
{

    public class Coordinates : TableData
    {
        public string latitude { get; set; }
        public string longitude { get; set; }
    }

    public class Location : TableData
    {
        [ForeignKey(typeof(Street))]
        public int StreetId { get; set; }
        [OneToOne(CascadeOperations = CascadeOperation.All)]
        public Street street { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }

        [ForeignKey(typeof(Coordinates))]
        public int CoordinatesID { get; set; }

        [OneToOne(CascadeOperations = CascadeOperation.All)]
        public Coordinates coordinates { get; set; }

        [ForeignKey(typeof (Timezone))]
        public int TimezoneID { get; set; }
        [OneToOne(CascadeOperations = CascadeOperation.All)]
        public Timezone timezone { get; set; }
    }

    public class Name : TableData
    {
        public string title { get; set; }
        public string first { get; set; }
        public string last { get; set; }
    }

    public class Picture : TableData
    {
        public string large { get; set; }
        public string medium { get; set; }
        public string thumbnail { get; set; }
    }

    public class Result : TableData
    {
        [ForeignKey(typeof(Root))]
        public int RootID { get; set; }

        [ForeignKey(typeof(Name))]
        public int NameId { get; set; }

        [OneToOne(CascadeOperations = CascadeOperation.All)]
        public Name name { get; set; }

        [ForeignKey(typeof(Location))]
        public int LocationID { get; set; }

        [OneToOne(CascadeOperations = CascadeOperation.All)]
        public Location location { get; set; }
        public string email { get; set; }

        [ForeignKey(typeof (Picture))]
        public int PictureID { get; set; }

        [OneToOne(CascadeOperations = CascadeOperation.All)]
        public Picture picture { get; set; }
        public double Rating { get; set; }
    }

    [Table("Root")]
    public class Root : TableData
    {
        [ForeignKey(typeof(Result))]
        public int ResultID { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Result> results { get; set; }
    }

    public class Street : TableData
    {
        public int number { get; set; }
        public string name { get; set; }
    }

    public class Timezone : TableData
    {
        public string offset { get; set; }
        public string description { get; set; }
    }



}
