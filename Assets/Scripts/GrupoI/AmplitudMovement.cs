using Navigation.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Navigation.World;

namespace GrupoI
{
    public class AmplitudMovement : INavigationAlgorithm
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
            
            private Directions _direccionActual = Directions.None;


            public void Initialize(WorldInfo informacionMundo, INavigationAlgorithm.AllowedMovements movimientosPermitidos)
            {
                _mundo = informacionMundo;
                //_dir = direccionesPermitidas;

            }

            public CellInfo[] GetPath(CellInfo startNode, CellInfo targetNode)
            {
                // Nodo en el que empieza el muñequito.
                Nodo nodoInicial = new Nodo(_mundo, startNode, null);
                
                Nodo actual;
                actual = _listaAbierta[0];
                _listaAbierta.Add(actual);      // Añadimos el estado inicial a la lista abierta
                
                while(_listaAbierta.Count!=0) //Mientras la lista no esté vacía.
                {
                    // Coger el primer elemento de la lista abierta.
                    actual = _listaAbierta[0];

                    // Metemos todos los nodos que hemos visitado en la lista cerrada.
                    _listaCerrada.Add(actual);
                    
                    // Comprobar si es la meta.
                    if(actual.esMeta())
                    {
                        //Devolver nodo meta.
                    } 
                    else 
                    {
                        List<Nodo> nodosExpandidos = actual.expandirNodo();     // Lista de nodos que se planean expandir
                        //Cambiar expandirNodo para que devuelva una lista de Nodos
                        //y no de CellInfos.
                        
                        //Meter nodosExpandidos en la lista abierta.
                        foreach (Nodo nodo in nodosExpandidos)          // Recorre la lista nodosExpandidod
                        {
                            for (int i = 0; i < _listaCerrada.Count; i++)
                            {
                            if (nodo != _listaCerrada[i]) {
                                _listaAbierta.Add(nodo); //Lo añade al final. }
                            }
                        }
                    }

                }
               

                CellInfo[] path = new CellInfo[1];

                CellInfo nextCell = conseguirVecino(startNode, _direccionActual); // Devuelve la celda vecina en la dirección indicada
                
                while (!nextCell.Walkable)
                {
                    
                }
                path[0] = nextCell;
                return path;
            }

    }
}




        // Poner limitador de tiempo para evitar que Unity crashee.

        //*/
    }
}
