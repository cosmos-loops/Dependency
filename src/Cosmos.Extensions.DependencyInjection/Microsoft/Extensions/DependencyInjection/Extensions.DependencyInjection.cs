﻿using System;
using Cosmos.Extensions.Dependency;
using Cosmos.Extensions.Dependency.Core;

namespace Microsoft.Extensions.DependencyInjection {
    /// <summary>
    /// Extensions for Dependency Injection
    /// </summary>
    public static class DependencyInjectionExtensions {
        /// <summary>
        /// Add Register Proxy
        /// </summary>
        /// <param name="services"></param>
        /// <param name="bag"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IServiceCollection AddRegisterProxyFrom(this IServiceCollection services, DependencyProxyRegister bag) {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (bag != null) {
                var descriptors = bag.ExportDescriptors();

                foreach (var descriptor in descriptors) {
                    switch (descriptor.ProxyType) {
                        case DependencyProxyType.TypeToType:
                            TypeToTypeRegister(services, descriptor);
                            break;

                        case DependencyProxyType.TypeToInstance:
                            TypeToInstanceRegister(services, descriptor);
                            break;

                        case DependencyProxyType.TypeToInstanceFunc:
                            TypeToInstanceFuncRegister(services, descriptor);
                            break;

                        case DependencyProxyType.TypeSelf:
                            TypeSelfRegister(services, descriptor);
                            break;

                        case DependencyProxyType.InstanceSelf:
                            InstanceSelfRegister(services, descriptor);
                            break;

                        case DependencyProxyType.InstanceSelfFunc:
                            InstanceSelfFuncRegister(services, descriptor);
                            break;

                        case DependencyProxyType.TypeToResolvedInstanceFunc:
                            TypeToResolvedInstanceFuncRegister(services, descriptor);
                            break;

                        case DependencyProxyType.ResolvedInstanceSelfFunc:
                            ResolvedInstanceSelfFuncRegister(services, descriptor);
                            break;
                    }
                }
            }

            return services;
        }

        private static void TypeToTypeRegister(IServiceCollection services, DependencyRegisterDescriptor d) {
            var lifetime = d.LifetimeType.ToMsLifetime();
            switch (lifetime) {
                case ServiceLifetime.Scoped:
                    services.AddScoped(d.ServiceType, d.ImplementationType);
                    break;

                case ServiceLifetime.Singleton:
                    services.AddSingleton(d.ServiceType, d.ImplementationType);
                    break;

                case ServiceLifetime.Transient:
                    services.AddTransient(d.ServiceType, d.ImplementationType);
                    break;

                default:
                    services.AddTransient(d.ServiceType, d.ImplementationType);
                    break;
            }
        }

        private static void TypeToInstanceRegister(IServiceCollection services, DependencyRegisterDescriptor d) {
            var lifetime = d.LifetimeType.ToMsLifetime();
            switch (lifetime) {
                case ServiceLifetime.Singleton:
                    services.AddSingleton(d.ServiceType, d.InstanceOfImplementation);
                    break;

                default:
                    throw new InvalidOperationException("Invalid operation for scoped or Transient mode.");
            }
        }

        private static void TypeToInstanceFuncRegister(IServiceCollection services, DependencyRegisterDescriptor d) {
            var lifetime = d.LifetimeType.ToMsLifetime();
            switch (lifetime) {
                case ServiceLifetime.Scoped:
                    services.AddScoped(d.ServiceType, p => d.InstanceFuncForImplementation());
                    break;

                case ServiceLifetime.Singleton:
                    services.AddSingleton(d.ServiceType, p => d.InstanceFuncForImplementation());
                    break;

                case ServiceLifetime.Transient:
                    services.AddTransient(d.ServiceType, p => d.InstanceFuncForImplementation());
                    break;

                default:
                    services.AddTransient(d.ServiceType, p => d.InstanceFuncForImplementation());
                    break;
            }
        }

        private static void TypeSelfRegister(IServiceCollection services, DependencyRegisterDescriptor d) {
            var lifetime = d.LifetimeType.ToMsLifetime();
            switch (lifetime) {
                case ServiceLifetime.Scoped:
                    services.AddScoped(d.ImplementationTypeSelf);
                    break;

                case ServiceLifetime.Singleton:
                    services.AddSingleton(d.ImplementationTypeSelf);
                    break;

                case ServiceLifetime.Transient:
                    services.AddTransient(d.ImplementationTypeSelf);
                    break;

                default:
                    services.AddTransient(d.ImplementationTypeSelf);
                    break;
            }
        }

        private static void InstanceSelfRegister(IServiceCollection services, DependencyRegisterDescriptor d) {
            var lifetime = d.LifetimeType.ToMsLifetime();
            switch (lifetime) {
                case ServiceLifetime.Singleton:
                    services.AddSingleton(d.InstanceOfImplementation);
                    break;

                default:
                    throw new InvalidOperationException("Invalid operation for scoped or Transient mode.");
            }
        }

        private static void InstanceSelfFuncRegister(IServiceCollection services, DependencyRegisterDescriptor d) {
            var lifetime = d.LifetimeType.ToMsLifetime();
            switch (lifetime) {
                case ServiceLifetime.Scoped:
                    services.AddScoped(p => d.InstanceFuncForImplementation());
                    break;
                
                case ServiceLifetime.Singleton:
                    services.AddSingleton(p => d.InstanceFuncForImplementation());
                    break;
                
                case ServiceLifetime.Transient:
                    services.AddTransient(p => d.InstanceFuncForImplementation());
                    break;

                default:
                    services.AddTransient(p => d.InstanceFuncForImplementation());
                    break;
            }
        }

        private static void TypeToResolvedInstanceFuncRegister(IServiceCollection services, DependencyRegisterDescriptor d) {
            if (d is DependencyRegisterDescriptor<IServiceProvider> d0) {
                var lifetime = d0.LifetimeType.ToMsLifetime();
                switch (lifetime) {
                    case ServiceLifetime.Scoped:
                        services.AddScoped(d0.ServiceType, p => d0.ResolveFuncForImplementation(p));
                        break;

                    case ServiceLifetime.Singleton:
                        services.AddSingleton(d0.ServiceType, p => d0.ResolveFuncForImplementation(p));
                        break;

                    case ServiceLifetime.Transient:
                        services.AddTransient(d0.ServiceType, p => d0.ResolveFuncForImplementation(p));
                        break;

                    default:
                        services.AddTransient(d0.ServiceType, p => d0.ResolveFuncForImplementation(p));
                        break;
                }
            }
        }

        private static void ResolvedInstanceSelfFuncRegister(IServiceCollection services, DependencyRegisterDescriptor d) {
            if (d is DependencyRegisterDescriptor<IServiceProvider> d0) {
                var lifetime = d0.LifetimeType.ToMsLifetime();
                switch (lifetime) {
                    case ServiceLifetime.Scoped:
                        services.AddScoped(p => d0.ResolveFuncForImplementation(p));
                        break;
                    
                    case ServiceLifetime.Singleton:
                        services.AddSingleton(p => d0.ResolveFuncForImplementation(p));
                        break;
                    
                    case ServiceLifetime.Transient:
                        services.AddTransient(p => d0.ResolveFuncForImplementation(p));
                        break;
                    
                    default:
                        services.AddTransient(p => d0.ResolveFuncForImplementation(p));
                        break;
                }
            }
        }
    }
}