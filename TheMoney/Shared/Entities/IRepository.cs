using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace TheMoney.Shared.Entities
{
    public interface IRepository
    {
        /* User queries */
        public IEnumerable<User> GetUsersWhere(Expression<Func<User, bool>> whereExpression);
        public User GetUserWhere(Expression<Func<User, bool>> whereExpression);
        public void ReplaceUserWhere(Expression<Func<User, bool>> whereExpression, User userToUpdate);
        public void InsertUser(User userToUpdate);

        /* Chart queries */
        public IEnumerable<Chart> GetChartsWhere(Expression<Func<Chart, bool>> whereExpression);
        public Chart GetChartWhere(Expression<Func<Chart, bool>> whereExpression);
        public void ReplaceChartWhere(Expression<Func<Chart, bool>> whereExpression, Chart chartToUpdate);
        public void InsertChart(Chart chartToUpdate);

        /* Monetary transaction queries */
        public IEnumerable<MonetaryTransaction> GetMonetaryTransactionsWhere(Expression<Func<MonetaryTransaction, bool>> whereExpression);
        public MonetaryTransaction GetMonetaryTransactionWhere(Expression<Func<MonetaryTransaction, bool>> whereExpression);
        public void InsertMonetaryTransaction(MonetaryTransaction monetaryTransactionToUpdate);
        public void InsertMonetaryTransactions(IEnumerable<MonetaryTransaction> monetaryTransactionsToUpdate);
    }
}
