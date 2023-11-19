using GrupoI;
using Navigation.Interfaces;
using Navigation.World;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using System.Numerics;

public class aEstrellaMovement : INavigationAlgorithm
{
    public enum Directions
    {
        None,
        Up,
        Right,
        Down,
        Left
    }

    private WorldInfo _mundo;
    private List<Nodo> _listaAbierta = new List<Nodo>();
    private List<Nodo> _listaCerrada = new List<Nodo>();
    private List<Nodo> _padresMeta = new List<Nodo>();
    private bool _meta = false;
    private int factor = 5;   // Limitado a examinar como máximo los 'factor' primeros elementos de la lista abierta
    


    public void Initialize(WorldInfo informacionMundo, INavigationAlgorithm.AllowedMovements movimientosPermitidos)
    {
        _mundo = informacionMundo;
        Debug.Log("iniciado!!");
    }

    public CellInfo[] GetPath(CellInfo startNode, CellInfo targetNode)
    {
        // Nodo en el que empieza el muñequito.
        Nodo nodoInicial = new Nodo(_mundo, startNode, null, 0);   // El padre del nodo actual es null
        Nodo nodoFinal = new Nodo(_mundo, targetNode, null, 0);

        _listaAbierta.Add(nodoInicial);      // Añadimos el estado inicial a la lista abierta

        Nodo actual = nodoInicial;

        while (!_meta)
        {
            Debug.Log("While iniciado!!");

            actual = _listaAbierta[0];  // Leemos al nodo actual el primer elemento
            _listaAbierta.RemoveAt(0);  // Eliminamos el primer elemento de la lista abierta

            // Guardamos todos los nodos que hemos visitado en la lista cerrada.
            _listaCerrada.Add(actual);

            if (actual.getInfoCelda().Equals(nodoFinal.getInfoCelda())/*actual.esMeta()*/) // Se ejecuta si el nodo actual es la meta
            {
                // While que guarde todos los padres del nodo meta en orden en una lista
                _padresMeta.Add(actual);                // Nodo meta
                while (actual.getPadre() != null)       // Se meten padres en la lista hasta llegar al nodo con padre null (nodo origen)
                {
                    _padresMeta.Add(actual.getPadre()); // Vector para  guardar todos los padres de abajo a ariba de la meta
                    actual = actual.getPadre();
                }
                _meta = true;   // La meta ha sido alcanzada y el while no se volverá a ejecutar
            }
            else               // Caso en el que el nodo actual no sea la meta
            {
                List<Nodo> nodosExpandidos = actual.expandirNodo();     // Lista de nodos puede expandir el actual
                Debug.Log("Nº de nodos expandidos "+nodosExpandidos.Count);
                //Meter nodosExpandidos en la lista abierta.
                foreach (Nodo nodo in nodosExpandidos)              // Recorre la lista nodosExpandidos
                {
                    bool count = false;
                    
                    for (int i = 0; i < _listaCerrada.Count; i++)      // Recorremos la lista cerrada para comprobar si nuestro nodo expandido candidato ya fué expandido
                    {
                        if (nodo.getInfoCelda().Equals(_listaCerrada[i].getInfoCelda()))
                        {     // Añadimos unicamente si estos no existian ya en la lista cerrada
                            count = true;
                            Debug.Log("El elemento ya estaba en la lista cerrada");
                            break;  // Salimos del bucle for. Una vez sabemos que está en la lista cerrada no hace falta seguir comprobando
                        }
                    }
                    if (!count)     // Se ejecuta si el elemento no estaba en la lista cerrada
                    {
                        // Meter el nodo en la posicion correspondiente ordenado con el fEstrella.
                        // Hay que tener en cuenta que la lista puede estar vacía
                        bool entre2 = false;
                        if (_listaAbierta.Count < 1)     // Si hay 0 elementos
                        {
                            _listaAbierta.Add(nodo);        // Añadimos a la lista directamente
                            entre2 = true;
                            Debug.Log("El nodo se ha añadido al final de la lista");
                        }
                        else if (!entre2)
                        {
                            // Se puede cambiar la variable 'factor'. El resultado es mas optimo si incrementamos su valor, pero el tiempo de cómputo también crecerá
                            for (int i = 0; i < Math.Min(_listaAbierta.Count, factor); i++)     // El nodo se añadirá delante del primer nodo de la lista abierta que sea mayor que el
                            {                                                               
                                Debug.Log("F* candidato "+ nodo.getFEstrella()+" vs F* de la posicion "+i+" " + _listaAbierta[i].getFEstrella());
                                if (nodo.getFEstrella() < _listaAbierta[i].getFEstrella())  // Si el elemento es menor, este se añade a la izquierda de con el que se comparó
                                {
                                    Debug.Log("Lista abierta tiene " + _listaAbierta.Count + " posiciones");
                                    _listaAbierta.Insert(i, nodo); //Insert recibe un índice. Mete el elemento antes de ese indice.
                                    entre2 = true;
                                    Debug.Log("Se ha añadido a la lista un elemento de x = " + nodo.getInfoCelda().x + " y = "+ nodo.getInfoCelda().y + " y una heurística de f* = "+ nodo.getFEstrella()+" en la posición "+i);
                                    Debug.Log("Lista abierta ahora tiene " + _listaAbierta.Count + " posiciones");
                                    break;  // Nos salimos del bloque for si ya hemos encontrado donde introducir el elemento
                                }
                            }
                        }
                        if (!entre2)           // Si el elemento no es menor a ninguno de los elementos comprobados de la lista, este se añadirá al final de la misma
                        {
                            _listaAbierta.Add(nodo);
                            entre2 = true;
                            Debug.Log("El nodo se ha añadido al final de la lista");
                        }
                    }
                }

            }
        }

        CellInfo[] path = new CellInfo[_padresMeta.Count]; ;    // Array de cell info con el tamaño de tantos padres como tenga el nodo meta
        for (int i = _padresMeta.Count-1, j = 0; i >= 0; i--, j++)
        {
            Debug.Log("Iteracion nº "+j+ " f* = " + _padresMeta[i].getFEstrella());    // Resumen de las iteraciones realizadas que llevan a la meta
            path[j] = _padresMeta[i].getInfoCelda();
        }
        return path;
    }
}
