﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Linq;

namespace Orm.Practice
{
    public class AddressRepositoryLinqImpl : RepositoryBase, IAddressRepository
    {
        public AddressRepositoryLinqImpl(ISession session)
            : base(session)
        {
        }

        public Address Get(int id)
        {
            #region Please implement the method

            return Session.Query<Address>().Single(a => a.Id == id);

            #endregion
        }

        public IList<Address> Get(IEnumerable<int> ids)
        {
            #region Please implement the method

            return Session.Query<Address>().Where(a => ids.Contains(a.Id)).ToList();

            #endregion
        }

        public IList<Address> GetByCity(string city)
        {
            #region Please implement the method

            return Session.Query<Address>().Where(a => a.City == city).ToList();

            #endregion
        }

        public async Task<IList<Address>> GetByCityAsync(string city)
        {
            #region Please implement the method

            return await Session.Query<Address>().Where(a => a.City == city).ToListAsync();

            #endregion
        }

        public async Task<IList<Address>> GetByCityAsync(
            string city, CancellationToken cancellationToken)
        {
            #region Please implement the method

            return await Session.Query<Address>().Where(a => a.City == city).ToListAsync(cancellationToken);

            #endregion
        }

        public IList<KeyValuePair<int, string>> GetOnlyTheIdAndTheAddressLineByCity(string city)
        {
            #region Please implement the method

            return Session.Query<Address>().Where(a => a.City == city).Select(a => new KeyValuePair<int, string>(a.Id, a.AddressLine1)).ToList();

            #endregion
        }

        public IList<string> GetPostalCodesByCity(string city)
        {
            #region Please implement the method

            return Session.Query<Address>().Where(a => a.City == city).Select(a => a.PostalCode).Distinct().ToList();

            #endregion
        }
    }
}