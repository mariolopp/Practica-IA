using GrupoI;
using Navigation.Interfaces;
using Navigation.World;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
    private List<Nodo> padresMeta = new List<Nodo>();
    private bool meta = false;
    


    public void Initialize(WorldInfo informacionMundo, INavigationAlgorithm.AllowedMovements movimientosPermitidos)
    {
        _mundo = informacionMundo;
        Debug.Log("iniciado!!");
    }

    public CellInfo[] GetPath(CellInfo startNode, CellInfo targetNode)
    {
        // Nodo en el que empieza el muñequito.
        Nodo nodoInicial = new Nodo(_mundo, startNode, null, 0);   // El padre del nodo actual es null

        _listaAbierta.Add(nodoInicial);      // Añadimos el estado inicial a la lista abierta

        Nodo actual = nodoInicial;

        while (!meta)
        {
            // Calcular distancia manhattan de los posibles sucesores del nodo a la meta
            Debug.Log("While iniciado!!");

            actual = _listaAbierta[0];
            _listaAbierta.RemoveAt(0);  // Eliminamos dicho elemento de la lista abierta

            // Metemos todos los nodos que hemos visitado en la lista cerrada.
            _listaCerrada.Add(actual);

            if (actual.esMeta())
            {
                // While que guarde todos los padres del nodo meta en orden en una lista
                padresMeta.Add(actual);
                while (actual.getPadre() != null)        // Se meten padres en la lista hasta llegar al nodo con padre null (nodo origen)
                {
                    padresMeta.Add(actual.getPadre());  // Vector para  guardar todos los padres de abajo a ariba de la meta de menor profundidad
                    actual = actual.getPadre();
                }
                meta = true;   // Eliminamos todos los elementos de la lista abierta para que no se ejecute mas el while
            }
            else
            {
                List<Nodo> nodosExpandidos = actual.expandirNodo();     // Lista de nodos expandidos a partir del actual

                //Meter nodosExpandidos en la lista abierta.
                foreach (Nodo nodo in nodosExpandidos)              // Recorre la lista nodosExpandidos
                {
                    bool count = false;
                    for (int i = 0; i < _listaCerrada.Count; i++)   
                    {
                        if (nodo.getInfoCelda() == _listaCerrada[i].getInfoCelda())
                        {     // Añadimos unicamente si estos no existian ya en la lista cerrada
                            count = true;
                            Debug.Log("Count es true!!");
                            break;  // Salimos del bucle for una vez sabemos que está en la lista cerrada no hace falta seguir comprobando
                        }
                    }
                    if (!count)     // Se ejecuta si el elemento no estaba ya en la lista cerrada
                    {
                        // Meter el nodo en la posicion correspondiente ordenado con el fEstrella.
                        // Hay que tener en cuenta que la lista esté vacía o solo tenga 1 elemento.
                        bool entre2 = false;
                        if (_listaAbierta.Count < 1)     // Si hay 0 elementos añadimos directamente al final
                        {
                            _listaAbierta.Add(nodo);
                            entre2 = true;
                        }
                        else if(!entre2)
                        {

                            //for (int i = 0; i < _listaAbierta.Count - 1; i++) // Si hay 1 elemento no se ejecuta, si hay 2 elementos se ejecuta 1 vez, si hay 3 se ejecuta 2 y asi...
                            //{
                            //    if (nodo.getFEstrella() >= _listaAbierta[i].getFEstrella() && nodo.getFEstrella() <= _listaAbierta[i + 1].getFEstrella())
                            //    {
                            //        _listaAbierta.Insert(i + 1, nodo); //Insert recibe un índice. Mete el elemento antes de ese indice.
                            //        entre2 = true;
                            //        break;
                            //    }
                            //}

                            // Se puede descomentar este for y comentar el if inmediatamente posterior a este for. El resultado no es el mismo pero se sigue llegando a la meta
                            //for (int i = 0; i < _listaAbierta.Count; i++) // Si hay 1 elemento se añadirá a su izquierda el nodo si es menor al elemento.
                            //                                              // Si hay 2 se compara si es menor que cada uno desde el principiio para añadirlo delante del primer caso en el que sea menor
                            //{
                            //    Debug.Log("For iniciado!!");
                            //    if (nodo.getFEstrella() < _listaAbierta[i].getFEstrella())  // Si el elemento es menor, este se añade a la izquierda de con el que se comparó
                            //    {
                            //        _listaAbierta.Insert(i, nodo); //Insert recibe un índice. Mete el elemento antes de ese indice.
                            //        entre2 = true;
                            //        break;  // Nos salimos del bloque for  
                            //    }
                            //}
                        }
                        if (nodo.getFEstrella() <= _listaAbierta[0].getFEstrella() && !entre2)
                        {    // Caso que deba ir delante cuando haya un solo elemento
                            _listaAbierta.Insert(0, nodo);  // Insertamos despues del primer elemento
                            entre2 = true;
                        }
                        else if (!entre2)           // Si el elemento no es menor a ninguno de los elementos de la lista este se añadirá al final de la misma
                        {
                            _listaAbierta.Add(nodo); // Caso que deba ir detras haya ninguno, uno, o varios elementos
                        }


                        /*
                       // FORMA DE SANDRO
                       _listaAbierta.Add(nodo);
                       //_listaAbierta = _listaAbierta.OrderBy(o => o.getFEstrella()).ToList();
                       _listaAbierta.Sort((obj1, obj2) => obj1.getFEstrella().CompareTo(obj2.getFEstrella()));
                       // */
                    }
                }

            }
        }

        CellInfo[] path = new CellInfo[padresMeta.Count]; ; // Devuelve la celda vecina en la dirección indicada
        for (int i = padresMeta.Count-1, j = 0; i >= 0; i--, j++)
        {
            Debug.Log("Iteracion nº "+j+ " f* = " + padresMeta[i].getFEstrella());
            path[j] = padresMeta[i].getInfoCelda();
        }


        return path;

        //Debug.Log("Distancia a la meta: "+actual.getInfoCelda().Distance(_mundo.Exit, CellInfo.DistanceType.Euclidean));
        //Debug.Log("fEstrella del nodoIncial: "+nodoInicial.getFEstrella());

        //CellInfo[] path = new CellInfo[1];
        //path[0] = _mundo[19,19];

    }

}
