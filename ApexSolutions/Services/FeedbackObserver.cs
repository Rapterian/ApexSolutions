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
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
        }

        public void Unsubscribe(IObserver<Feedback> observer)
        {
            if (_observers.Contains(observer))
            {
                _observers.Remove(observer);
            }
        }

        public void OnNext(Feedback feedback)
        {
            _latestFeedback = feedback;
            NotifyObservers();
        }

        public void OnError(Exception error)
        {
            // Handle errors here if needed
        }

        public void OnCompleted()
        {
            // Handle completion if necessary
        }

        private void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.OnNext(_latestFeedback);
            }
        }
    }
}
