using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Booking.Calendar.API.Models.Dto;
using Dapper;

namespace Booking.Calendar.API.Application.Queries
{
    public class CalendarQueries : ICalendarQueries
    {
        private string _connectionString = string.Empty;

        public CalendarQueries(string constr)
        {
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }

        public async Task<Apponintment> GetAppointmentAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var result = await connection.QueryAsync<Apponintment>(
                   @"select *
                        FROM apponintment a                        
                        WHERE a.Id=@id"
                        , new { id }
                    );

                if (result.AsList().Count == 0)
                    throw new KeyNotFoundException();

                return MapItem(result);
            }
        }

        public async Task<IEnumerable<Apponintment>> GetAppointmentsFromUserAsync(string user, string categoria, DateTime date)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                return await connection.QueryAsync<Apponintment>(@"select *
                        FROM apponintment a                        
                        WHERE a.To=@id AND a.Categoria=@categoria AND CAST(a.StartDate AS DATE) = CAST(@date AS DATE)"
                        , new { user ,categoria, date }
                    );
            }
        }

        private Apponintment MapItem(dynamic result)
        {
            var app = new Apponintment
            {
                Id = result[0].Id,
                StartDate = result[0].StartDate,
                Categoria = result[0].Categoria,
                ClassEvent = result[0].ClassEvent,
                Description = result[0].Description,
                From = result[0].From,
                Title = result[0].Title,
                To = result[0].To
            };
            return app;
        }
        }
}
