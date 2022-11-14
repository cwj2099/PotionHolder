using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    private Image _image;
    public Image Image
    {
        get { return _image; }
        set { _image = value; }
    }

    private int _health; 
    public int Health
    {
        get { return _health; }
        set { _health = value; }
    }

    private void Awake()
    {
        _image = GetComponent<Image>();
        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 1);

        _health = 100;
    }

    public void SetHealth(int _newHealth)
    {
        _health = _newHealth;
        _image.fillAmount = (float)_health / 20;
    }
}
