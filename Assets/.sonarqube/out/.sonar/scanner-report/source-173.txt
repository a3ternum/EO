using System;
using System.Collections;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CreatureTests
{
    private GameObject creatureGameObject;
    private Creature creature;

    [SetUp]
    public void SetUp()
    {
        creatureGameObject = new GameObject();
        creature = creatureGameObject.AddComponent<Creature>();
        creature.creatureStats = ScriptableObject.CreateInstance<CreatureStats>();
    }

    [TearDown]
    public void TearDown()
    {
        UnityEngine.Object.Destroy(creatureGameObject);
    }

    private object InvokeProtectedMethod(object instance, string methodName, params object[] parameters)
    {
        MethodInfo method = instance.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
        if (method == null)
        {
            throw new ArgumentException($"Method '{methodName}' not found in type '{instance.GetType().FullName}'");
        }
        return method.Invoke(instance, parameters);
    }

    [Test]
    public void InitializeCreatureStats_SetsCorrectValues()
    {
        // Arrange
        creature.creatureStats.healthBase = 100;
        creature.creatureStats.healthFlat = 50;
        creature.creatureStats.healthIncreases = 0.2f;
        creature.creatureStats.healthMoreMultipliers = 0.1f;

        // Act
        InvokeProtectedMethod(creature, "InitializeCreatureStats");

        // Assert
        Assert.AreEqual(165, creature.currentMaxHealth);
    }

    [Test]
    public void InitializeHealthBar_HealthBarIsInitialized()
    {
        // Act
        InvokeProtectedMethod(creature, "InitializeHealthBar");

        // Assert
        Assert.IsNotNull(creature.healthBarComponent);
    }

    [Test]
    public void TakeDamage_ReducesHealth()
    {
        // Arrange
        creature.currentHealth = 100;
        float[] damage = new float[] { 10, 0, 0, 0, 0 };
        Creature attacker = new GameObject().AddComponent<Creature>();

        // Act
        creature.TakeDamage(damage, attacker);

        // Assert
        Assert.Less(creature.currentHealth, 100);
    }

    [Test]
    public void Die_DestroysGameObject()
    {
        // Arrange
        creature.currentHealth = 0;

        // Act
        InvokeProtectedMethod(creature, "Die");

        // Assert
        Assert.IsTrue(creature == null);
    }

    //[UnityTest]
    //public IEnumerator ApplyIgnite_AppliesDamageOverTime()
    //{
    //    // Arrange
    //    creature.currentHealth = 100;
    //    float igniteDamage = 20;

    //    // Act
    //    yield return (IEnumerator)InvokeProtectedMethod(creature, "ApplyIgnite", igniteDamage);

    //    // Assert
    //    Assert.Less(creature.currentHealth, 100);
    //}

    //[UnityTest]
    //public IEnumerator ApplyChill_AppliesChillEffect()
    //{
    //    // Arrange
    //    float chillEffect = 0.2f;
    //    float duration = 2f;

    //    // Act
    //    yield return (IEnumerator)InvokeProtectedMethod(creature, "ApplyChill", chillEffect, duration);

    //    // Assert
    //    Assert.IsFalse(creature.isChilled);
    //}

    //[UnityTest]
    //public IEnumerator ApplyFreeze_AppliesFreezeEffect()
    //{
    //    // Arrange
    //    float duration = 2f;

    //    // Act
    //    yield return (IEnumerator)InvokeProtectedMethod(creature, "ApplyFreeze", duration);

    //    // Assert
    //    Assert.IsFalse(creature.isFrozen);
    //}

    //[UnityTest]
    //public IEnumerator ApplyShock_AppliesShockEffect()
    //{
    //    // Arrange
    //    float shockEffect = 0.2f;
    //    float duration = 2f;

    //    // Act
    //    yield return (IEnumerator)InvokeProtectedMethod(creature, "ApplyShock", shockEffect, duration);

    //    // Assert
    //    Assert.IsFalse(creature.isShocked);
    //}

    //[UnityTest]
    //public IEnumerator ResetDamagedRecently_ResetsDamagedRecentlyFlag()
    //{
    //    // Arrange
    //    float time = 2f;
    //    creature.damagedRecently = true;

    //    // Act
    //    yield return (IEnumerator)InvokeProtectedMethod(creature, "ResetDamagedRecently", time);

    //    // Assert
    //    Assert.IsFalse(creature.damagedRecently);
    //}
}