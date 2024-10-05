namespace ValorRiseClient;
using System.Numerics;
using MongoDB.Bson;
using Riptide;
using ValorRise;

public static class MessageExtensions
{
  #region Character
  public static Message AddCharacter(this Message message, Character character)
  {
    message.AddObjectId(character.Id);
    message.AddObjectId(character.OwnerId);
    message.AddString(character.Name);
    message.AddInt(character.Level);
    message.AddInt(character.Exp);
    message.AddDouble(character.X);
    message.AddDouble(character.Y);
    message.AddString(character.CreatedAt.ToString());

    return message;
  }

  public static Character GetCharacter(this Message message)
  {
    return new Character()
    {
      Id = message.GetObjectId(),
      OwnerId = message.GetObjectId(),
      Name = message.GetString(),
      Level = message.GetInt(),
      Exp = message.GetInt(),
      X = message.GetDouble(),
      Y = message.GetDouble(),
      CreatedAt = DateTime.Parse(message.GetString())
    };
  }
  #endregion

  #region CharacterInfo
  public static Message AddCharacterInfo(this Message message, CharacterInfo character)
  {
    message.AddObjectId(character.Id);
    message.AddObjectId(character.OwnerId);
    message.AddString(character.Name);
    message.AddInt(character.Level);
    message.AddInt(character.Exp);
    message.AddString(character.CreatedAt.ToString());

    return message;
  }

  public static CharacterInfo GetCharacterInfo(this Message message)
  {
    return new CharacterInfo()
    {
      Id = message.GetObjectId(),
      OwnerId = message.GetObjectId(),
      Name = message.GetString(),
      Level = message.GetInt(),
      Exp = message.GetInt(),
      CreatedAt = DateTime.Parse(message.GetString())
    };
  }

  public static CharacterInfo[] GetCharacterInfos(this Message message, int count)
  {
    var infos = new CharacterInfo[count];
    for (int i = 0; i < count; i++)
    {
      infos[i] = message.GetCharacterInfo();
    }
    return infos;
  }
  #endregion

  #region User
  public static Message AddUser(this Message message, User user)
  {
    message.AddObjectId(user.Id);
    message.AddString(user.Username);
    message.AddString(user.Email);
    message.AddString(user.PasswordHash);
    message.AddString(user.Salt);
    message.AddStrings(user.Roles);
    message.AddString(user.CreatedAt.ToString());

    return message;
  }

  public static User GetUser(this Message message)
  {
    return new User()
    {
      Id = message.GetObjectId(),
      Username = message.GetString(),
      Email = message.GetString(),
      PasswordHash = message.GetString(),
      Salt = message.GetString(),
      Roles = message.GetStrings(),
      CreatedAt = DateTime.Parse(message.GetString())
    };
  }
  #endregion

  #region ObjectId
  public static Message AddObjectId(this Message message, ObjectId value)
  {
    message.AddString(value.ToString());
    return message;
  }

  public static ObjectId GetObjectId(this Message message)
  {
    return ObjectId.Parse(message.GetString());
  }
  #endregion

  #region Vector2
  /// <inheritdoc cref="Add(Message, Vector2)"/>
  /// <remarks>Relying on the correct Add overload being chosen based on the parameter type can increase the odds of accidental type mismatches when retrieving data from a message. This method calls <see cref="Add(Message, Vector2)"/> and simply provides an alternative type-explicit way to add a <see cref="Vector2"/> to the message.</remarks>
  public static Message AddVector2(this Message message, Vector2 value) => Add(message, value);

  /// <summary>Adds a <see cref="Vector2"/> to the message.</summary>
  /// <param name="value">The <see cref="Vector2"/> to add.</param>
  /// <returns>The message that the <see cref="Vector2"/> was added to.</returns>
  public static Message Add(this Message message, Vector2 value)
  {
    message.AddFloat(value.X);
    message.AddFloat(value.Y);
    return message;
  }

  /// <summary>Retrieves a <see cref="Vector2"/> from the message.</summary>
  /// <returns>The <see cref="Vector2"/> that was retrieved.</returns>
  public static Vector2 GetVector2(this Message message)
  {
    return new Vector2(message.GetFloat(), message.GetFloat());
  }
  #endregion

  #region Vector3
  /// <inheritdoc cref="Add(Message, Vector3)"/>
  /// <remarks>Relying on the correct Add overload being chosen based on the parameter type can increase the odds of accidental type mismatches when retrieving data from a message. This method calls <see cref="Add(Message, Vector3)"/> and simply provides an alternative type-explicit way to add a <see cref="Vector3"/> to the message.</remarks>
  public static Message AddVector3(this Message message, Vector3 value) => Add(message, value);

  /// <summary>Adds a <see cref="Vector3"/> to the message.</summary>
  /// <param name="value">The <see cref="Vector3"/> to add.</param>
  /// <returns>The message that the <see cref="Vector3"/> was added to.</returns>
  public static Message Add(this Message message, Vector3 value)
  {
    message.AddFloat(value.X);
    message.AddFloat(value.Y);
    message.AddFloat(value.Z);
    return message;
  }

  /// <summary>Retrieves a <see cref="Vector3"/> from the message.</summary>
  /// <returns>The <see cref="Vector3"/> that was retrieved.</returns>
  public static Vector3 GetVector3(this Message message)
  {
    return new Vector3(message.GetFloat(), message.GetFloat(), message.GetFloat());
  }
  #endregion

  #region Quaternion
  /// <inheritdoc cref="Add(Message, Quaternion)"/>
  /// <remarks>Relying on the correct Add overload being chosen based on the parameter type can increase the odds of accidental type mismatches when retrieving data from a message. This method calls <see cref="Add(Message, Quaternion)"/> and simply provides an alternative type-explicit way to add a <see cref="Quaternion"/> to the message.</remarks>
  public static Message AddQuaternion(this Message message, Quaternion value) => Add(message, value);

  /// <summary>Adds a <see cref="Quaternion"/> to the message.</summary>
  /// <param name="value">The <see cref="Quaternion"/> to add.</param>
  /// <returns>The message that the <see cref="Quaternion"/> was added to.</returns>
  public static Message Add(this Message message, Quaternion value)
  {
    message.AddFloat(value.X);
    message.AddFloat(value.Y);
    message.AddFloat(value.Z);
    message.AddFloat(value.W);
    return message;
  }

  /// <summary>Retrieves a <see cref="Quaternion"/> from the message.</summary>
  /// <returns>The <see cref="Quaternion"/> that was retrieved.</returns>
  public static Quaternion GetQuaternion(this Message message)
  {
    return new Quaternion(message.GetFloat(), message.GetFloat(), message.GetFloat(), message.GetFloat());
  }
  #endregion
}