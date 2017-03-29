using UnityEngine;
using System.Collections;

public class ElfController : MonoBehaviour {

    //переменная для установки макс. скорости персонажа
    public float speed = 3f;
    //переменная для определения направления персонажа вправо/влево
    private bool isFacingRight = true;
    //ссылка на компонент анимаций
    private Animator anim;
    private float fastspeed;

    public Vector2 curentSavePos;

    /// <summary>
    /// Начальная инициализация
    /// </summary>
	private void Start()
    {
        fastspeed = speed;
        anim = GetComponent<Animator>();
    }

    /// <summary>
    /// Выполняем действия в методе FixedUpdate, т. к. в компоненте Animator персонажа
    /// выставлено значение Animate Physics = true и анимация синхронизируется с расчетами физики
    /// </summary>
    private void FixedUpdate()
    {
        //используем Input.GetAxis для оси Х. метод возвращает значение оси в пределах от -1 до 1.
        //при стандартных настройках проекта 
        //-1 возвращается при нажатии на клавиатуре стрелки влево (или клавиши А),
        //1 возвращается при нажатии на клавиатуре стрелки вправо (или клавиши D)
 //       float move = Input.GetAxis("Horizontal");
        float transV = Input.GetAxis("Vertical") * fastspeed * Time.deltaTime;
        float transH = Input.GetAxis("Horizontal") * fastspeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftShift))
            fastspeed = 6f;
        else
            fastspeed = speed;

        transform.Translate(new Vector3(transH, transV, 0));
        //в компоненте анимаций изменяем значение параметра Speed на значение оси Х.
        //приэтом нам нужен модуль значения
           anim.SetFloat("Speed", Mathf.Abs(transH));

        //обращаемся к компоненту персонажа RigidBody2D. задаем ему скорость по оси Х, 
        //равную значению оси Х умноженное на значение макс. скорости
        //  rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);

        //если нажали клавишу для перемещения вправо, а персонаж направлен влево
        if (transH > 0 && !isFacingRight)
            //отражаем персонажа вправо
            Flip();
        //обратная ситуация. отражаем персонажа влево
        else if (transH < 0 && isFacingRight)
            Flip();
    }

    /// <summary>
    /// Метод для смены направления движения персонажа и его зеркального отражения
    /// </summary>
    private void Flip()
    {
        //меняем направление движения персонажа
        isFacingRight = !isFacingRight;
        //получаем размеры персонажа
        Vector3 theScale = transform.localScale;
        //зеркально отражаем персонажа по оси Х
        theScale.x *= -1;
        //задаем новый размер персонажа, равный старому, но зеркально отраженный
        transform.localScale = theScale;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "savepoint")
            curentSavePos = transform.position;
        if (other.tag == "killpoint")
            transform.position = curentSavePos;
    }
}