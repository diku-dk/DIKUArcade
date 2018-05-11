using System;
using DIKUArcade.EventBus;

namespace DIKUArcade.Timers {
    public class TimedEventContainer {
        /// <summary>
        /// Every field in the container will be a tuple of a boolean,
        /// indicating if the data is being used, and a struct containing
        /// the data.
        /// </summary>
        internal struct TimedEventTuple {
            public bool occupied;
            public TimedEvent timedEvent;
        }

        private readonly TimedEventTuple[] events;
        private readonly int size;

        private GameEventBus<object> eventBus;

        public TimedEventContainer(int size) {
            if (size < 1) {
                throw new ArgumentOutOfRangeException($"Size ({size}) must be a positive integer");
            }

            events = new TimedEventTuple[size];
            this.size = size;
            ResetContainer();
        }

        public void AttachEventBus(GameEventBus<object> bus) {
            eventBus = bus;
        }

        /// <summary>
        /// Clear this container of all bound TimedEvent objects
        /// </summary>
        public void ResetContainer() {
            for (int i = 0; i < size; i++) {
                events[i].occupied = false;
            }
        }

        /// <summary>
        /// Add a timed event to this container, if there is enough space
        /// </summary>
        public void AddTimedEvent(TimeSpanType type, int timeSpan, string message,
            string parameter1, string parameter2) {
            for (int i = 0; i < size; i++) {
                ref var Tuple = ref events[i];
                ref var Event = ref Tuple.timedEvent;
                if (!Tuple.occupied) {
                    Tuple.occupied = true;
                    Event = new TimedEvent(type, timeSpan, message, parameter1, parameter2);
                    Event.ResetTimer();
                    return;
                }
            }
        }

        /// <summary>
        /// Iterate through its internal list of timed events and for each,
        /// push the event to the registered EventBus if its time has elapsed.
        /// </summary>
        public void ProcessTimedEvents() {
            for (int i = 0; i < size; i++) {
                ref var Tuple = ref events[i];
                ref var Event = ref Tuple.timedEvent;

                if (Tuple.occupied && Event.HasExpired()) { // boolean short-circuitry!
                    Tuple.occupied = false;
                    eventBus.RegisterEvent(GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.TimedEvent, this, Event.message, Event.parameter1,
                        Event.parameter2));
                }
            }
        }
    }
}