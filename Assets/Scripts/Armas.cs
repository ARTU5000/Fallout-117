using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TipoArma
{
    Espada,
    Arco,
    Rifle
}

public struct DatosArma
{
    public string nombre;
    public int daño;
    public TipoArma tipo;

    public DatosArma(string nombre, int daño, TipoArma tipo)
    {
        this.nombre = nombre;
        this.daño = daño;
        this.tipo = tipo;
    }

    public override string ToString()
    {
        return $"Nombre del arma: {nombre}, Daño: {daño}, Tipo: {tipo}.";
    }
}
public class Armas : MonoBehaviour
{
    DatosArma arma1 = new DatosArma("Espada de Baphomet", 10, TipoArma.Espada);
    DatosArma arma2 = new DatosArma("Arco del Sol", 25, TipoArma.Arco);
    DatosArma arma3 = new DatosArma("Rifle estandar", 325, TipoArma.Rifle);
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(arma1);
        Debug.Log(arma2);
        Debug.Log(arma3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
