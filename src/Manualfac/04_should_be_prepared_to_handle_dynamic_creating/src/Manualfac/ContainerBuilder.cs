﻿using System;
using System.Collections.Generic;

namespace Manualfac
{
    public class ContainerBuilder
    {
        readonly List<IRegistrationBuilder> registrations = new List<IRegistrationBuilder>();
        bool hasBeenBuilt;

        public IRegistrationBuilder RegisterComponent(ComponentRegistration registration)
        {
            if (registration == null) { throw new ArgumentNullException(nameof(registration)); }
            var builder = new RegistrationBuilder
            {
                Activator = registration.Activator,
                Service = registration.Service
            };

            registrations.Add(builder);
            return builder;
        }

        public Container Build()
        {
            if (hasBeenBuilt)
            {
                throw new InvalidOperationException("The container has been built.");
            }

            var registry = new ComponentRegistry();
            foreach (IRegistrationBuilder builder in registrations)
            {
                ComponentRegistration registration = builder.Build();
                registry.Register(registration);
            }

            hasBeenBuilt = true;
            return new Container(registry);
        }
    }
}