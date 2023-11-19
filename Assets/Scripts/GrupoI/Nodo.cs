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

        private float g;

        private float fEstrella;

        // Constructor.
        public Nodo(WorldInfo informacion, CellInfo celda, Nodo padre, float g)
        {
            this.infoCelda = celda;
            this.informacionMundo = informacion;
            this.padre = padre;
            this.g = g;
            fEstrella = g + this.infoCelda.Distance(this.informacionMundo.Exit, CellInfo.DistanceType.Euclidean); // G del nodo + su distancia euclidea a la meta
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

            Nodo nodoDerecha = new Nodo(informacionMundo, derecha, this, g+1);
            Nodo nodoIzquierda = new Nodo(informacionMundo, izquierda, this, g+1);
            Nodo nodoArriba = new Nodo(informacionMundo, arriba, this, g+1);
            Nodo nodoAbajo = new Nodo(informacionMundo, abajo, this, g+1);


            //¿¿¿¿¿EVITAR CICLOS SIMPLES???????

            if (derecha.Walkable)
            {
                //nodoDerecha.fEstrella = (19 - nodoDerecha.infoCelda.x) + (19 - nodoDerecha.infoCelda.y) +g;
                nodosExpandidos.Add(nodoDerecha);
            }
            if (izquierda.Walkable)
            {
                //nodoIzquierda.fEstrella = 19 - nodoIzquierda.infoCelda.x + 19 - nodoIzquierda.infoCelda.y+g;
                nodosExpandidos.Add(nodoIzquierda);
            }
            if (arriba.Walkable)
            {
                //nodoArriba.fEstrella = 19 - nodoArriba.infoCelda.x + 19 - nodoArriba.infoCelda.y+g;
                nodosExpandidos.Add(nodoArriba);
            }
            if (abajo.Walkable)
            {
                //nodoAbajo.fEstrella = 19 - nodoAbajo.infoCelda.x + 19 - nodoAbajo.infoCelda.y+g;
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

        public float getFEstrella() {
            return fEstrella;
        }
        
    }
}

