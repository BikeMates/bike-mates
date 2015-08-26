using BikeMates.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BikeMates.Contracts.Services
{
  public  class RoutesSearchParameters
    {
     public List<Route> entity{get;set;}
     public string Location{get;set;}
     public string Dist1{get;set;}
     public string Dist2{get;set;}
     public string Date1{get;set;}
     public string Date2 { get; set; }
     public bool Date{ get; set; }
     public bool Name{ get; set; }
     public bool Participants { get; set; }
    }
}
