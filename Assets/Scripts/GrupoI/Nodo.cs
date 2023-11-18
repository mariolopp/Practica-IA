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

        private float fEstrella;

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
                    //nodoDerecha.hestrella = 19 - nodoDerecha.infoCelda.x + 19 - nodoDerecha.infoCelda.y;
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
        
    }
}

