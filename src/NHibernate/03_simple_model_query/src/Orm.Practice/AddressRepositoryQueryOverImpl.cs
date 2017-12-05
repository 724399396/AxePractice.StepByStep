using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using NHibernate.Transform;

namespace Orm.Practice
{
    public class AddressRepositoryQueryOverImpl : RepositoryBase, IAddressRepository
    {
        public AddressRepositoryQueryOverImpl(ISession session) : base(session)
        {
        }

        public Address Get(int id)
        {
            #region Please implement the method

            return Session.QueryOver<Address>().Where(a => a.Id == id).SingleOrDefault();

            #endregion
        }

        public IList<Address> Get(IEnumerable<int> ids)
        {
            #region Please implement the method

            return Session.QueryOver<Address>().Where(Restrictions.On<Address>(a => a.Id).IsIn(ids.ToList())).List();

            #endregion
        }

        public IList<Address> GetByCity(string city)
        {
            #region Please implement the method

            return Session.QueryOver<Address>().Where(a => a.City == city).OrderBy(a => a.Id).Asc.List();
            #endregion
        }

        public Task<IList<Address>> GetByCityAsync(string city)
        {
            #region Please implement the method

            return Session.QueryOver<Address>().Where(a => a.City == city).OrderBy(a => a.Id).Asc.ListAsync();

            #endregion
        }

        public Task<IList<Address>> GetByCityAsync(string city, CancellationToken cancellationToken)
        {
            #region Please implement the method

            return Session.QueryOver<Address>().Where(a => a.City == city).ListAsync(cancellationToken);

            #endregion
        }

        public IList<KeyValuePair<int, string>> GetOnlyTheIdAndTheAddressLineByCity(string city)
        {
            #region Please implement the method

            return Session.QueryOver<Address>()
                .OrderBy(a => a.Id).Asc
                .Where(a => a.City == city).SelectList(list => list.Select(x => x.Id).Select(x => x.AddressLine1))
                .TransformUsing(new ResultTransformer(x => new KeyValuePair<int,string>((int) x[0], (string) x[1]), null))
                .List<KeyValuePair<int, string>>();

            #endregion
        }

        public IList<string> GetPostalCodesByCity(string city)
        {
            #region Please implement the method

            return Session.QueryOver<Address>().Where(a => a.City == city).SelectList(a => a.SelectGroup(x => x.PostalCode)).List<string>();

            #endregion
        }
    }
}