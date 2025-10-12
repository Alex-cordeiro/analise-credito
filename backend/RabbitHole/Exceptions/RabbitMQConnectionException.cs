using System;

namespace RabbitHole.Exceptions;

public class RabbitMQConnectionException : Exception
{
  public RabbitMQConnectionException(string message, Exception innerException) : base(message, innerException)
  {
    
  }
}
