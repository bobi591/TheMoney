using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MongoDB.Driver;
using TheMoney.Shared.Entities;

namespace TheMoney.Datastore.Databases.MongoDB.Application
{
    public sealed class MongoDBRepository : IRepository
    {
        private MongoDBServices _mongoDBServices;

        public MongoDBRepository(IMongoDBSettings applicationDatabaseSettings)
        {
            this._mongoDBServices = new MongoDBServices(applicationDatabaseSettings);
        }

        /* User queries */

        public IEnumerable<User> GetUsersWhere(Expression<Func<User, bool>> whereExpression)
        {
            try
            {
                return this._mongoDBServices.Users.FindSync(whereExpression).ToEnumerable();
            }
            catch (ArgumentNullException)
            {
                return new List<User>();
            }
        }

        public User GetUserWhere(Expression<Func<User, bool>> whereExpression)
        {
            return this._mongoDBServices.Users.FindSync(whereExpression).FirstOrDefault();
        }

        public void InsertUser(User userToUpdate)
        {
            this._mongoDBServices.Users.InsertOne(userToUpdate);
        }

        public void ReplaceUserWhere(Expression<Func<User, bool>> whereExpression, User userToUpdate)
        {
            User originalUserDocument = GetUserWhere(whereExpression);
            if (originalUserDocument != null)
            {
                string originalUserId = originalUserDocument.Id;
                userToUpdate.Id = originalUserId;
                this._mongoDBServices.Users.ReplaceOne(whereExpression, userToUpdate);
            }
        }

        /* Chart queries */

        public IEnumerable<Chart> GetChartsWhere(Expression<Func<Chart, bool>> whereExpression)
        {
            try
            {
                return this._mongoDBServices.Charts.FindSync(whereExpression).ToEnumerable();
            }
            catch(ArgumentNullException)
            {
                return new List<Chart>();
            } 

        }

        public Chart GetChartWhere(Expression<Func<Chart, bool>> whereExpression)
        {
            return this._mongoDBServices.Charts.FindSync(whereExpression).FirstOrDefault();
        }

        public void InsertChart(Chart chartToUpdate)
        {
            this._mongoDBServices.Charts.InsertOne(chartToUpdate);
        }

        public void ReplaceChartWhere(Expression<Func<Chart, bool>> whereExpression, Chart chartToUpdate)
        {
            Chart originalChartDocument = GetChartWhere(whereExpression);
            if (originalChartDocument != null)
            {
                string originalChartId = originalChartDocument.Id;
                chartToUpdate.Id = originalChartId;
                this._mongoDBServices.Charts.ReplaceOne(whereExpression, chartToUpdate);
            }
        }
    }
}
