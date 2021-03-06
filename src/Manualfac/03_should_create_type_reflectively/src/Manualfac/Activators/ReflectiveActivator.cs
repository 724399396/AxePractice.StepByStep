﻿using System;
using System.Linq;
using System.Reflection;
using Manualfac.Services;

namespace Manualfac.Activators
{
    class ReflectiveActivator : IInstanceActivator
    {
        readonly Type serviceType;

        public ReflectiveActivator(Type serviceType)
        {
            this.serviceType = serviceType;
        }

        #region Please modify the following code to pass the test

        /*
         * This method create instance via reflection. Try evaluating its parameters and
         * inject them using componentContext.
         * 
         * No public members are allowed to add.
         */

        public object Activate(IComponentContext componentContext)
        {
            var constructors = serviceType.GetConstructors();
            if (constructors.Length != 1)
            {
                throw new DependencyResolutionException();
            }
            var constructor = constructors.First();
            ParameterInfo[] parametesTypess = constructor.GetParameters();
            var parameters = parametesTypess.Select(p => componentContext.ResolveComponent(new TypedService(p.ParameterType))).ToArray();
            return Activator.CreateInstance(serviceType, parameters);
        }

        #endregion
    }
}