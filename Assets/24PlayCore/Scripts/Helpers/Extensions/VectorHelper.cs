using UnityEngine;

public static class VectorHelper
{
    /// <summary>
    /// Возвращает true если расстояние между векторами меньше заданного
    /// </summary>
    /// <param name="firstVector"> Первый вектор</param>
    /// <param name="secondVector"> Второй вектор</param>
    /// <param name="distance"> Расстояние</param>
    /// <returns></returns>
    public static bool InRange(Vector3 firstVector, Vector3 secondVector, float distance)
    {
        return SqrDistance(firstVector, secondVector) < distance * distance;
    }

    /// <summary>
    /// Возвращает квадрат расстояния между векторами
    /// </summary>
    /// <param name="firstVector"> Первый вектор</param>
    /// <param name="secondVector"> Второй вектор</param>
    /// <returns></returns>
    /// 
    public static float SqrDistance(Vector3 firstVector, Vector3 secondVector)
    {
        return (firstVector - secondVector).sqrMagnitude;
    }

    /// <summary>
    /// Возвращает направление от одного вектора к другому
    /// </summary>
    /// <param name="from"> Вектор от которого считается направление</param>
    /// <param name="to"> Вектор к которому считается направление</param>
    /// <returns></returns>
    public static Vector3 Direction(Vector3 from, Vector3 to)
    {
        return to - from;
    }

    /// <summary>
    /// Возвращает true если расстояние между векторами меньше заданного
    /// </summary>
    /// <param name="firstVector"> Первый вектор</param>
    /// <param name="secondVector"> Второй вектор</param>
    /// <param name="distance"> Расстояние</param>
    /// <returns></returns>
    public static bool InRange(Vector2 firstVector, Vector2 secondVector, float distance)
    {
        return SqrDistance(firstVector, secondVector) < distance * distance;
    }

    /// <summary>
    /// Возвращает квадрат расстояния между векторами
    /// </summary>
    /// <param name="firstVector"> Первый вектор</param>
    /// <param name="secondVector"> Второй вектор</param>
    /// <returns></returns>
    public static float SqrDistance(Vector2 firstVector, Vector2 secondVector)
    {
        return (firstVector - secondVector).sqrMagnitude;
    }

    /// <summary>
    /// Возвращает направление от одного вектора к другому
    /// </summary>
    /// <param name="from"> Вектор от которого считается направление</param>
    /// <param name="to"> Вектор к которому считается направление</param>
    /// <returns></returns>
    public static Vector2 Direction(Vector2 from, Vector2 to)
    {
        return to - from;
    }

    /// <summary>
    /// Повертає рандомну позицію всередині круга із заданим радіусом
    /// </summary>
    /// <param name="maxRadius"> Максимальний радіус</param>
    /// <param name="minRadius"> Мінімальний радіус</param>
    /// <returns></returns>
    public static Vector2 RandomPositionInCircle2D(float maxRadius, float minRadius = 0f)
    {
        var direction = Random.insideUnitCircle.normalized;
        var distance = Random.Range(minRadius, maxRadius);

        return direction * distance;
    }

    /// <summary>
    /// Повертає рандомну позицію всередині круга із заданим радіусом
    /// </summary>
    /// <param name="maxRadius"> Максимальний радіус</param>
    /// <param name="minRadius"> Мінімальний радіус</param>
    /// <param name="y"> Y кінцевої позиції</param>
    /// <returns></returns>
    public static Vector3 RandomPositionInCircle(float maxRadius, float minRadius = 0f, int y = 0)
    {
        var pos2D = RandomPositionInCircle2D(maxRadius, minRadius);
        return new Vector3(pos2D.x, y, pos2D.y);
    }

    /// <summary>
    /// Повертає рандомну позицію всередині круга із заданим радіусом
    /// </summary>
    /// <param name="center"> Центр круга</param>
    /// <param name="maxRadius"> Максимальний радіус</param>
    /// <param name="minRadius"> Мінімальний радіус</param>
    /// <param name="y"> Y кінцевої позиції</param>
    /// <returns></returns>
    public static Vector3 RandomPositionInCircle(Vector3 center, float maxRadius, float minRadius = 0f, int y = 0)
    {
        return center + RandomPositionInCircle(maxRadius, minRadius, y);
    }

    /// <summary>
    /// Повертає рандомну позицію всередині сфери із заданим радіусом
    /// </summary>
    /// <param name="maxRadius"> Максимальний радіус</param>
    /// <param name="minRadius"> Мінімальний радіус</param>
    /// <returns></returns>
    public static Vector3 RandomPositionInSphere(float maxRadius, float minRadius = 0f)
    {
        var direction = Random.insideUnitSphere.normalized;
        var distance = Random.Range(minRadius, maxRadius);

        return direction * distance;
    }

    /// <summary>
    /// Повертає рандомну позицію всередині сфери із заданим радіусом
    /// </summary>
    /// <param name="center"> Центр сфери</param>
    /// <param name="maxRadius"> Максимальний радіус</param>
    /// <param name="minRadius"> Мінімальний радіус</param>
    /// <returns></returns>
    public static Vector3 RandomPositionInSphere(Vector3 center, float maxRadius, float minRadius = 0f)
    {
        return center + RandomPositionInSphere(maxRadius, minRadius);
    }
}