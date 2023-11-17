using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Navigation.World;
using static Navigation.World.CellInfo;
using UnityEditor.Experimental.GraphView;

namespace GrupoI
{
    public class Nodo
    {

        private CellInfo infoCelda;
        
        private WorldInfo informacionMundo;

        private Nodo padre;

        //private Direction direction;

        // Constructor.
        public Nodo(WorldInfo informacion, CellInfo celda, Nodo padre)
        {
            this.infoCelda = celda;
            this.informacionMundo = informacion;
            this.padre = padre;
        }

        // Devuelve true si es el nodo meta.
        public bool esMeta()
        {
            CellInfo meta = informacionMundo.Exit;
            return meta.Equals(this.infoCelda);
        }

        // Devuelve una lista con los nodos expandidos de este nodo.
        public List<Nodo> expandirNodo()
        {
            List<Nodo> nodosExpandidos = new List<Nodo>();

            CellInfo derecha = informacionMundo[infoCelda.x+1, infoCelda.y];
            CellInfo izquierda = informacionMundo[infoCelda.x-1, infoCelda.y];
            CellInfo arriba = informacionMundo[infoCelda.x, infoCelda.y-1];
            CellInfo abajo = informacionMundo[infoCelda.x, infoCelda.y+1];

            Nodo nodoDerecha = new Nodo(informacionMundo, derecha, this);
            Nodo nodoIzquierda = new Nodo(informacionMundo, izquierda, this);
            Nodo nodoArriba = new Nodo(informacionMundo, arriba, this);
            Nodo nodoAbajo = new Nodo(informacionMundo, abajo, this);


            //¿¿¿¿¿EVITAR CICLOS SIMPLES???????

            if (derecha.Walkable)
                {
                    nodosExpandidos.Add(nodoDerecha);
                }
                if (izquierda.Walkable)
                {
                    nodosExpandidos.Add(nodoIzquierda);
                }
                if (arriba.Walkable)
                {
                    nodosExpandidos.Add(nodoArriba);
                }
                if (abajo.Walkable)
                {
                    nodosExpandidos.Add(nodoAbajo);
                }
                

            return nodosExpandidos;
        }

        public Nodo getPadre() {
            return padre;
        }


        public CellInfo getInfoCelda() { 
            return infoCelda;
        
        }

        public WorldInfo getWorldInfo() {
            return informacionMundo;
        }



        //----------------ANTERIOR-----------------


        /*
        private CellInfo infoCelda; // Es la información de la celda en la que está este nodo.
                            // Contiene la x, la y, el tipo de celda y un método (Walkable) que devuelve true si es caminable.
        private WorldInfo informacionMundo; // Necesario para expandir los vecinos.

        private int fasterisco; // f* = g + h*

        private Nodo padre; // Para no expandir a un nodo padre.
        
        // IMPORTANTE: para comprobar cellInfo no usar == sino Equals().

        public Nodo(WorldInfo informacion, CellInfo celda, Nodo padre)
        {
            this.informacionMundo = informacion;
            this.infoCelda = celda;
            this.padre = padre;
        }
        

        ArrayList expandirNodo()
        {
            ArrayList listaNodos = new ArrayList();
            if(!esMeta())
            {
                
            }
            return listaNodos;
            // Si no es meta, el nodo debe expandir a sus vecinos, pero solo los que son caminables, walkables.
            // Llamará a conseguirVecinos() para expandir.
            // Devolverá una lista con los nodos que ha expandido. En AEstrellaMovement tiene que haber una lista, en
        }

        bool esMeta()
        {
            // Comprobar si este nodo es la meta.
            return (infoCelda.Type == CellType.Exit);
        }

        public Nodo conseguirVecino(CellInfo current, Directions direction) //Para expandir necesitamos saber los vecinos.
        {
            Nodo vecino = new Nodo();

            if (vecino == this.padre) // Comprobar que no sea el padre.
            {

            }

            switch (direction)
            {

                // Nota: informacionMundo[current.x, current.y - 1] devuelve un CellInfo.
                case Directions.Up:
                    vecino = new Nodo(informacionMundo, informacionMundo[current.x, current.y - 1], this); //Devuelve la celda que está encima.
                    break;
                case Directions.Right:
                    // Celda que está a la derecha.
                    vecino = new Nodo(informacionMundo, informacionMundo[current.x + 1, current.y], this);
                    //vecino = _world[current.x + 1, current.y];
                    break;
                case Directions.Down:
                    // Celda que está abajo.
                    vecino = new Nodo(informacionMundo, informacionMundo[current.x, current.y + 1], this);
                    //vecino = _world[current.x, current.y + 1];
                    break;
                default:
                    vecino = new Nodo(informacionMundo, informacionMundo[current.x - 1, current.y], this);
                    //vecino = _world[current.x - 1, current.y];
                    break;
            }

            // CONTINUAR!!!!!

            return vecino;
        }

        public void distanciaManhattan(Nodo otroNodo){
            // Calcula la distancia Manhattan entre este nodo y otro nodo, el otroNodo.
            return 
        }

        */
    }
}

