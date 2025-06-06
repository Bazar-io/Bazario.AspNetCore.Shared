﻿using Bazario.AspNetCore.Shared.Abstractions.DomainEvents;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;

namespace Bazario.AspNetCore.Shared.Infrastructure.DomainEvents
{
    internal sealed class DomainEventDispatcher(
        IServiceScopeFactory serviceScopeFactory)
        : IDomainEventDispatcher
    {
        private static readonly ConcurrentDictionary<Type, Type> HandlerTypeDictionary = new();
        private static readonly ConcurrentDictionary<Type, Type> WrapperTypeDictionary = new();

        public async Task DispatchAsync(
            IDomainEvent domainEvent,
            CancellationToken cancellationToken = default)
        {
            using IServiceScope scope = serviceScopeFactory.CreateScope();

            Type domainEventType = domainEvent.GetType();
            Type handlerType = HandlerTypeDictionary.GetOrAdd(
                domainEventType,
                et => typeof(IDomainEventHandler<>).MakeGenericType(et));

            IEnumerable<object?> handlers = scope.ServiceProvider.GetServices(handlerType);

            foreach (object? handler in handlers)
            {
                if (handler is null)
                {
                    continue;
                }

                var handlerWrapper = HandlerWrapper.Create(handler, domainEventType);

                await handlerWrapper.Handle(domainEvent, cancellationToken);
            }
        }

        private abstract class HandlerWrapper
        {
            public abstract Task Handle(IDomainEvent domainEvent, CancellationToken cancellationToken);

            public static HandlerWrapper Create(object handler, Type domainEventType)
            {
                Type wrapperType = WrapperTypeDictionary.GetOrAdd(
                    domainEventType,
                    et => typeof(HandlerWrapper<>).MakeGenericType(et));

                return (HandlerWrapper)Activator.CreateInstance(wrapperType, handler)!;
            }
        }

        private sealed class HandlerWrapper<T>(object handler) : HandlerWrapper where T : IDomainEvent
        {
            private readonly IDomainEventHandler<T> _handler = (IDomainEventHandler<T>)handler;

            public override async Task Handle(IDomainEvent domainEvent, CancellationToken cancellationToken)
            {
                await _handler.Handle((T)domainEvent, cancellationToken);
            }
        }
    }
}
