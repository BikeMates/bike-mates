using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BikeMates.Contracts;
using BikeMates.Contracts.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using BikeMates.Domain.Entities;

namespace BikeMates.DataAccess.Repository
{
    public class RouteRepository : IRouteRepository //TODO: Create a base Repository class and move common logic into it
    {
        private readonly BikeMatesDbContext context;

        public RouteRepository(BikeMatesDbContext context)
        {
            this.context = context;
        }

        public RouteRepository()
        {
            // TODO: Complete member initialization
        }
        public void Add(Route entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                this.context.Set<Route>().Add(entity);
                this.context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx) //TODO: Remove duplicated code. Follow DRY principle
            {
                string errorMessage = string.Empty;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errorMessage += string.Format("Property: {0} Error: {1}",
                        validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
                    }
                }
                throw new Exception(errorMessage, dbEx); //TODO: Do not throw general exceptions. Remove this code or create custom Exception class
            }
        }

        public void Delete(Route entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                this.context.Set<Route>().Remove(entity);
                SaveChanges();
            } 
            catch (DbEntityValidationException dbEx)
            {
                string errorMessage = string.Empty;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errorMessage += string.Format("Property: {0} Error: {1}",
                        validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
                    }
                }
                throw new Exception(errorMessage, dbEx);
            }
        }

        public IEnumerable<Route> GetAll()
        {
            return this.context.Routes.ToList();
        }

        public Route Get(int id)
        {
            return this.context.Set<Route>().Find(id);
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }

        public void Update(Route entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                } 
                this.context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            } 
            catch (DbEntityValidationException dbEx)
            {
                string errorMessage = string.Empty;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errorMessage += string.Format("Property: {0} Error: {1}",
                        validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
                    }
                }
                throw new Exception(errorMessage, dbEx);
            }
        }
        public List<Route> GetAllRoutes()
        {
            List<Route> RouteViewList = new List<Route>();

            //List<Route> allRoutes = context.Routes.ToList();

            for (int rot = 0; rot <= 10; rot++)
            {
                RouteViewList.Add(new Route()
                {
                    /* Name=rot.Name,
                     MeetingPlace=rot.MeetingPlace,
                     Distance=rot.Distance,
                     Description=rot.Description,
                     Start=rot.Start,
                     ParticipantsCount=rot.ParticipantsCount*/
                    Name = "Route Name",
                    MeetingPlace = "Lviv st. Patona",
                    Distance = 20,
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    Start = new DateTime(26 / 07 / 2015),
                    ParticipantsCount = 5
                });
            }
            return RouteViewList;
        }
    }
}
