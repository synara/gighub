using GigHubOk.Controllers;
using GigHubOk.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace GigHubOk.ViewModels
{
    public class GigFormViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Venue { get; set; }
        
        [Required]
        [FutureDate]
        public string Date { get; set; }

        [Required]
        [ValidTime]
        public string Time { get; set; }

        [Required]
        public int Genre { get; set; }

        public IEnumerable<Genre> Genres { get; set; }

        public string Heading { get; set; }

        public string Action
        {
            get
            {
                //forma mais segura de chamar os métodos update e create
                //retorna o nome da função que tá em GigsController

                Expression<Func<GigsController, ActionResult>> create = (c => c.Create(this));
                Expression<Func<GigsController, ActionResult>> update = (c => c.Update(this));

                var action = (Id == 0 ? create : update);

                //retorna o nome da função ou nulo
                return (action.Body as MethodCallExpression).Method.Name;
            }
        }

        public DateTime GetDateTime() {
            return DateTime.Parse(string.Format("{0} {1}", Date, Time));

        }
    }
}