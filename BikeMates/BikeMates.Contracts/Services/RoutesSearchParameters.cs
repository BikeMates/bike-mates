using BikeMates.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BikeMates.Contracts.Services
{
  public  class RoutesSearchParameters //TODO: Move to domain
    {
     public List<Route> entity{get;set;} //TODO: rename to Routes
     public string Location{get;set;}
     public string Dist1 { get; set; } //TODO: rename DistanceMin
     public string Dist2{get;set;}
     public string Date1{get;set;} //TODO: rename to DateFrom
     public string Date2 { get; set; }
     //TODO: replace fields below with RoutsSortingBy enum or string
     public bool Date{ get; set; }
     public bool Name{ get; set; }
     public bool Participants { get; set; }
    }
}
