using System;
using System.Collections.Generic;
using ApexSolutions.Interfaces;
using ApexSolutions.Models;

namespace ApexSolutions.Services
{
    public class FeedbackObserver : IObserver<Feedback>
    {
        private readonly List<IObserver<Feedback>> _observers;
        private Feedback _latestFeedback;

        public FeedbackObserver()
        {
            _observers = new List<IObserver<Feedback>>();
        }

        public void Subscribe(IObserver<Feedback> observer)
        {
            if (observer == null)
            {
                Console.WriteLine("Cannot subscribe a null observer.");
                return;
            }

            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
            else
            {
                Console.WriteLine("Observer is already subscribed.");
            }
        }

        public void Unsubscribe(IObserver<Feedback> observer)
        {
            if (observer == null)
            {
                Console.WriteLine("Cannot unsubscribe a null observer.");
                return;
            }

            if (_observers.Remove(observer))
            {
                Console.WriteLine("Observer unsubscribed successfully.");
            }
            else
            {
                Console.WriteLine("Observer was not found in the subscription list.");
            }
        }

        public void OnNext(Feedback feedback)
        {
            if (feedback == null)
            {
                Console.WriteLine("Received null feedback.");
                return;
            }

            _latestFeedback = feedback;
            NotifyObservers();
        }

        public void OnError(Exception error)
        {
            if (error == null)
            {
                Console.WriteLine("Received a null error.");
                return;
            }

            Console.WriteLine($"An error occurred: {error.Message}");
        }

        public void OnCompleted()
        {
            Console.WriteLine("Feedback processing is complete.");
        }

        private void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                try
                {
                    observer.OnNext(_latestFeedback);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to notify an observer: {ex.Message}");
                }
            }
        }
    }
}