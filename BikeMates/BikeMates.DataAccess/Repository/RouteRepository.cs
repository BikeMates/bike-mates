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
    public class RouteRepository : IRouteRepository
    {
        private readonly BikeMatesDbContext context;

        public RouteRepository(BikeMatesDbContext context)
        {
            this.context = context;
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
    }
}
