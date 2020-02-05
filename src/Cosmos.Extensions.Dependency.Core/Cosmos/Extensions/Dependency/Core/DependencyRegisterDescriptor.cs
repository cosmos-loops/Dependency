﻿using System;

namespace Cosmos.Extensions.Dependency.Core {
    /// <summary>
    /// Register proxy descriptor
    /// </summary>
    public class DependencyRegisterDescriptor {
        internal Type RegisterType { get; set; }

        /// <summary>
        /// Service type
        /// </summary>
        public Type ServiceType { get; set; }

        /// <summary>
        /// Implementation type
        /// </summary>
        public Type ImplementationType { get; set; }

        /// <summary>
        /// Implementation type self
        /// </summary>
        public Type ImplementationTypeSelf { get; set; }

        /// <summary>
        /// Instance of implementation
        /// </summary>
        public object InstanceOfImplementation { get; set; }

        /// <summary>
        /// Instance func for implementation
        /// </summary>
        public Func<object> InstanceFuncForImplementation { get; set; }

        /// <summary>
        /// Proxy type
        /// </summary>
        public DependencyProxyType ProxyType { get; set; }

        /// <summary>
        /// Lifetime type
        /// </summary>
        public DependencyLifetimeType LifetimeType { get; set; }

        /// <summary>
        /// Create
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        /// <param name="lifetimeType"></param>
        /// <returns></returns>
        public static DependencyRegisterDescriptor Create<TService, TImplementation>(DependencyLifetimeType lifetimeType) {
            return new DependencyRegisterDescriptor {
                RegisterType = typeof(TService),
                ServiceType = typeof(TService),
                ImplementationType = typeof(TImplementation),
                ProxyType = DependencyProxyType.TypeToType,
                LifetimeType = lifetimeType
            };
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        /// <param name="instance"></param>
        /// <param name="lifetimeType"></param>
        /// <returns></returns>
        public static DependencyRegisterDescriptor Create<TService, TImplementation>(TImplementation instance, DependencyLifetimeType lifetimeType) {
            return new DependencyRegisterDescriptor {
                RegisterType = typeof(TService),
                ServiceType = typeof(TService),
                InstanceOfImplementation = instance,
                ProxyType = DependencyProxyType.TypeToInstance,
                LifetimeType = lifetimeType
            };
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="instance"></param>
        /// <param name="lifetimeType"></param>
        /// <returns></returns>
        public static DependencyRegisterDescriptor Create<TService>(object instance, DependencyLifetimeType lifetimeType) {
            return new DependencyRegisterDescriptor {
                RegisterType = typeof(TService),
                ServiceType = typeof(TService),
                InstanceOfImplementation = instance,
                ProxyType = DependencyProxyType.TypeToInstance,
                LifetimeType = lifetimeType
            };
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <typeparam name="TImplementationSelf"></typeparam>
        /// <param name="lifetimeType"></param>
        /// <returns></returns>
        public static DependencyRegisterDescriptor Create<TImplementationSelf>(DependencyLifetimeType lifetimeType) {
            return new DependencyRegisterDescriptor {
                RegisterType = typeof(TImplementationSelf),
                ImplementationTypeSelf = typeof(TImplementationSelf),
                ProxyType = DependencyProxyType.TypeSelf,
                LifetimeType = lifetimeType
            };
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="implementationType"></param>
        /// <param name="lifetimeType"></param>
        /// <returns></returns>
        public static DependencyRegisterDescriptor Create(object instance, Type implementationType, DependencyLifetimeType lifetimeType) {
            return new DependencyRegisterDescriptor {
                RegisterType = implementationType,
                InstanceOfImplementation = instance,
                ProxyType = DependencyProxyType.InstanceSelf,
                LifetimeType = lifetimeType
            };
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        /// <param name="instanceFunc"></param>
        /// <param name="lifetimeType"></param>
        /// <returns></returns>
        public static DependencyRegisterDescriptor Create<TService, TImplementation>(Func<TImplementation> instanceFunc, DependencyLifetimeType lifetimeType) {
            return new DependencyRegisterDescriptor {
                RegisterType = typeof(TService),
                ServiceType = typeof(TService),
                ImplementationType = typeof(TImplementation),
                InstanceFuncForImplementation = () => instanceFunc(),
                ProxyType = DependencyProxyType.TypeToInstanceFunc,
                LifetimeType = lifetimeType
            };
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="instanceFunc"></param>
        /// <param name="lifetimeType"></param>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TResolver"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        /// <returns></returns>
        public static DependencyRegisterDescriptor
            Create<TService, TResolver, TImplementation>(Func<TResolver, TImplementation> instanceFunc, DependencyLifetimeType lifetimeType) {
            return new DependencyRegisterDescriptor<TResolver> {
                RegisterType = typeof(TService),
                ServiceType = typeof(TService),
                ImplementationType = typeof(TImplementation),
                ResolveFuncForImplementation = resolver => instanceFunc(resolver),
                ProxyType = DependencyProxyType.TypeToResolvedInstanceFunc,
                LifetimeType = lifetimeType
            };
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="instanceFunc"></param>
        /// <param name="lifetimeType"></param>
        /// <returns></returns>
        public static DependencyRegisterDescriptor Create<TService>(Func<object> instanceFunc, DependencyLifetimeType lifetimeType) {
            return new DependencyRegisterDescriptor {
                RegisterType = typeof(TService),
                ServiceType = typeof(TService),
                InstanceFuncForImplementation = instanceFunc,
                ProxyType = DependencyProxyType.TypeToInstanceFunc,
                LifetimeType = lifetimeType
            };
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="instanceFunc"></param>
        /// <param name="lifetimeType"></param>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TResolver"></typeparam>
        /// <returns></returns>
        public static DependencyRegisterDescriptor Create<TService, TResolver>(Func<TResolver, object> instanceFunc, DependencyLifetimeType lifetimeType) {
            return new DependencyRegisterDescriptor<TResolver> {
                RegisterType = typeof(TService),
                ServiceType = typeof(TService),
                ResolveFuncForImplementation = instanceFunc,
                ProxyType = DependencyProxyType.TypeToResolvedInstanceFunc,
                LifetimeType = lifetimeType
            };
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <typeparam name="TImplementation"></typeparam>
        /// <param name="instanceFunc"></param>
        /// <param name="lifetimeType"></param>
        /// <returns></returns>
        public static DependencyRegisterDescriptor Create<TImplementation>(Func<TImplementation> instanceFunc, DependencyLifetimeType lifetimeType) {
            return new DependencyRegisterDescriptor {
                RegisterType = typeof(TImplementation),
                ImplementationTypeSelf = typeof(TImplementation),
                InstanceFuncForImplementation = () => instanceFunc(),
                ProxyType = DependencyProxyType.InstanceSelfFunc,
                LifetimeType = lifetimeType
            };
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="instanceFunc"></param>
        /// <param name="lifetimeType"></param>
        /// <typeparam name="TResolver"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        /// <returns></returns>
        public static DependencyRegisterDescriptor Create<TResolver, TImplementation>(Func<TResolver, TImplementation> instanceFunc, DependencyLifetimeType lifetimeType) {
            return new DependencyRegisterDescriptor<TResolver> {
                RegisterType = typeof(TImplementation),
                ImplementationTypeSelf = typeof(TImplementation),
                ResolveFuncForImplementation = resolver => instanceFunc(resolver),
                ProxyType = DependencyProxyType.ResolvedInstanceSelfFunc,
                LifetimeType = lifetimeType
            };
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="instanceFunc"></param>
        /// <param name="implementationType"></param>
        /// <param name="lifetimeType"></param>
        /// <returns></returns>
        public static DependencyRegisterDescriptor Create(Func<object> instanceFunc, Type implementationType, DependencyLifetimeType lifetimeType) {
            return new DependencyRegisterDescriptor {
                RegisterType = implementationType,
                InstanceFuncForImplementation = instanceFunc,
                ProxyType = DependencyProxyType.InstanceSelfFunc,
                LifetimeType = lifetimeType
            };
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="instanceFunc"></param>
        /// <param name="implementationType"></param>
        /// <param name="lifetimeType"></param>
        /// <typeparam name="TResolver"></typeparam>
        /// <returns></returns>
        public static DependencyRegisterDescriptor Create<TResolver>(Func<TResolver, object> instanceFunc, Type implementationType, DependencyLifetimeType lifetimeType) {
            return new DependencyRegisterDescriptor<TResolver> {
                RegisterType = implementationType,
                ResolveFuncForImplementation = instanceFunc,
                ProxyType = DependencyProxyType.ResolvedInstanceSelfFunc,
                LifetimeType = lifetimeType
            };
        }
    }
}