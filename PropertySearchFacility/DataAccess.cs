using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic.CompilerServices;
using MySql.Data.MySqlClient;
using MySqlConnectionLibrary;

namespace SykesCottagesInterviewTest
{
    public class DataAccess
    {
        public MySqlMain SqlMain = new MySqlMain();
        public MySqlDataReader SqlReader;

        public void Connect()
        {
            SqlMain.Connect("localhost", "sykes_interview", "Administrator", "password");
        }

        
        public void Query(string location, int nearBeach, int acceptsPets, int sleeps, int beds, string availableFrom, string availableTo)
        {
            // Without pattern matching
            /*
            SqlReader = SqlMain.Query($@"USE sykes_interview;
                SELECT *
                FROM properties
                INNER JOIN locations
                ON properties._fk_location = locations.__pk
                LEFT JOIN bookings
                ON bookings._fk_property = properties.__pk   
                WHERE
                locations.location_name = '{location}' AND
                properties.near_beach = {nearBeach} AND
                properties.accepts_pets = {acceptsPets} AND
                properties.sleeps <= {sleeps} AND
                properties.beds >= {beds} AND
                ('{availableFrom}' > bookings.end_date OR
                '{availableTo}' < bookings.start_date OR
                bookings.end_date IS NULL OR
                bookings.start_date IS NULL)
                GROUP BY properties.property_name");
            */
            // With basic pattern matching
            SqlReader = SqlMain.Query($@"USE sykes_interview;
                SELECT *
                FROM properties
                INNER JOIN locations
                ON properties._fk_location = locations.__pk
                LEFT JOIN bookings
                ON bookings._fk_property = properties.__pk   
                WHERE
                ('{location}' LIKE '%orks%' OR '{location}' LIKE '%otland%' OR '{location}' LIKE '%ornwa%' OR '{location}' LIKE '%ales%') AND
                properties.near_beach = {nearBeach} AND
                properties.accepts_pets = {acceptsPets} AND
                properties.sleeps <= {sleeps} AND
                properties.beds >= {beds} AND
                ('{availableFrom}' > bookings.end_date OR
                '{availableTo}' < bookings.start_date OR
                bookings.end_date IS NULL OR
                bookings.start_date IS NULL)
                GROUP BY properties.property_name");
        }
    }
}


