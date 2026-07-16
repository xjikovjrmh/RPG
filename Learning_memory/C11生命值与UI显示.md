添加UI动画 取名TextUpdate  

选中文本Text ui， 添加Animator， 如图 ，不添加，使用任何参数

![image-20260716125248049](C:\Users\Lenovo\AppData\Roaming\Typora\typora-user-images\image-20260716125248049.png)

对TextUpdate 取消勾选loop time

```csharp
public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public int currentHealth;
    public int maxHealth;
    public TMP_Text healthText;
    public Animator healthTextAnim;       //这里是动画机  外部要拖入的是Text 而不是动画TextUpdate

    private void Start()
    {
        currentHealth = maxHealth;
        healthText.text = "HP: "+currentHealth+"/"+maxHealth;
    }

    public void ChangeHealth(int amount)  //正代表治疗，负数代表受伤
    {
        currentHealth += amount;
        healthTextAnim.Play("TextUpdate");       // 这里播放动画
        healthText.text = "HP: " + currentHealth + "/" + maxHealth;
        if (currentHealth<=0)
        {
            gameObject.SetActive(false);
        }
    }
    
}

```

