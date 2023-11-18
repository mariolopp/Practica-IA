using Navigation.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Navigation.World;
using UnityEditor.VersionControl;

namespace GrupoI
{
    public class AmplitudMovement
    {
        //*
        public enum AllowedMovements
        {
            FourDirections,
            EightDirections
        }

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
            
            private Directions _direccionActual = Directions.None;

            private int nodeCount = 0;

            private bool meta = false;

        public void Initialize(WorldInfo informacionMundo, INavigationAlgorithm.AllowedMovements movimientosPermitidos)
            {
                _mundo = informacionMundo;
            //_dir = direccionesPermitidas;
            Debug.Log("iniciado!!");

        }

            public CellInfo[] GetPath(CellInfo startNode, CellInfo targetNode)
            {
                // Nodo en el que empieza el muñequito.
                Nodo nodoInicial = new Nodo(_mundo, startNode, null, 0);   // El padre del nodo actual es null
                
                
                Nodo actual = nodoInicial;      // Comenzamos con el nodo inicial como nodo actual

            //actual = _listaAbierta[0];      //  NO entiendo que pinta esto ?
            _listaAbierta.Add(nodoInicial);      // Añadimos el estado inicial a la lista abierta

             while (!meta) // Mientras no se haya encontrado una meta
            {
                Debug.Log("While iniciado!!");
                // Coger el primer elemento de la lista abierta.
                actual = _listaAbierta[0];  
                _listaAbierta.RemoveAt(0);  // Eliminamos dicho elemento de la lista abierta

                // Metemos todos los nodos que hemos visitado en la lista cerrada.
                _listaCerrada.Add(actual);

                // Comprobar si es la meta.
                if (actual.esMeta())
                {
                    // While que guarde todos los padres del nodo meta en orden en una lista
                    padresMeta.Add(actual);
                    while (actual.getPadre() != null)        // Se meten padres en la lista hasta llegar al nodo con padre null (nodo origen)
                    {
                        padresMeta.Add(actual.getPadre());  // Vector para  guardar todos los padres de abajo a ariba de la meta de menor profundidad
                        actual = actual.getPadre();
                    }
                    Debug.Log("Count es true!!  " + padresMeta[0].getInfoCelda().x);
                    Debug.Log("Count es true y vale!!  " + padresMeta.Count);
                    //padresMeta.Reverse();   // Ordena la lista de forma que el ultimo padre en expandir ahora será el primero para recorrerla en orden mas tarde
                    meta =true;   // Eliminamos todos los elementos de la lista abierta para que no se ejecute mas el while

                    Debug.Log("Meta!!");
                }
                else
                {
                    List<Nodo> nodosExpandidos = actual.expandirNodo();     // Lista de nodos expandidos a partir del actual

                    //Meter nodosExpandidos en la lista abierta.
                    foreach (Nodo nodo in nodosExpandidos)          // Recorre la lista nodosExpandidos
                    {
                        bool count = false;
                        for (int i = 0; i < _listaCerrada.Count; i++)
                        {
                            if (nodo.getInfoCelda() == _listaCerrada[i].getInfoCelda())
                            {     // Añadimos unicamente si estos no existian ya en la lista cerrada
                                count = true;
                                Debug.Log("Count es true!!");
                            }
                        }
                        if (!count)
                        {
                            _listaAbierta.Add(nodo);        // Lo añade al final de la lista abierta
                        }
                    }

                }
            }

            // Creamos una lista de vecinos en formato cellinfo

            CellInfo[] path = new CellInfo[padresMeta.Count]; ; // Devuelve la celda vecina en la dirección indicada
            for (int i = padresMeta.Count-1, j = 0; i >= 0; i--, j++)
            {
                Debug.Log("Iteracion nº "+i);
                path[j] = padresMeta[i].getInfoCelda();
            }
            //for (int i = 0; i < padresMeta.Count; i++)
            //{
            //    Debug.Log("For iniciado!!");
            //    path[i] = padresMeta[i].getInfoCelda();
            //}
            //Debug.Log("Count es true!!  "+ padresMeta[0].getInfoCelda().x);
            //path[0] = obtenerVecino(nodoInicial.getInfoCelda(), Directions.Up);
            //path[0] = nodoInicial.getInfoCelda();
            //path[0] = padresMeta[nodeCount].getInfoCelda();
            //path[0] = padresMeta[nodeCount].getInfoCelda();
            //    nodeCount++;

            for(int i=0; i<padresMeta.Count; i++)
            {
                Debug.Log("Nodo nº " + i + "Posicion x: "+padresMeta[i].getInfoCelda().x + ", " + padresMeta[i].getInfoCelda().y);
            }

            return path;
            }
        //public CellInfo obtenerVecino(CellInfo current, Directions direction)
        //{
        //    CellInfo neighbour;

        //    switch (direction)
        //    {
        //        case Directions.Up:
        //            neighbour = _mundo[current.x, current.y - 1];
        //            break;
        //        case Directions.Right:
        //            neighbour = _mundo[current.x + 1, current.y];
        //            break;
        //        case Directions.Down:
        //            neighbour = _mundo[current.x, current.y + 1];
        //            break;
        //        default:
        //            neighbour = _mundo[current.x - 1, current.y];
        //            break;
        //    }

        //    return neighbour;
        //}
    }
}

        // Poner limitador de tiempo para evitar que Unity crashee.

        //*/

