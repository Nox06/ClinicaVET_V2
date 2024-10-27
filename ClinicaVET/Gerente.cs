using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace ClinicaVET
{
    static class Gerente
    {
        public static List<Cliente> ListaCliente = new List<Cliente>();
        public static List<Mascotas> ListaMascota = new List<Mascotas>();
        public static int op;

        public static void guardar()
        {
            string path = @"C:\Users\yedic\Desktop\ClinicaVET_V2\ClinicaVET_V2\listaCliente.dat";
            IFormatter formatter = new BinaryFormatter();
            using (Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                formatter.Serialize(stream, ListaCliente);
            }
            Console.WriteLine("Lista de clientes guardada en binario.");
        }

        public static void Leer()
        {
            string path = @"C:\Users\yedic\Desktop\ClinicaVET_V2\ClinicaVET_V2\listaCliente.dat";
            if (File.Exists(path))
            {
                IFormatter formatter = new BinaryFormatter();
                using (Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    ListaCliente = (List<Cliente>)formatter.Deserialize(stream);
                }
                Console.WriteLine("Lista de clientes leída desde binario.");
            }
            else
            {
                Console.WriteLine("No se encontró el archivo binario. Se creó una nueva lista.");
            }
        }

        public static bool verificar(int ID)
        {
            foreach (Cliente aux in ListaCliente)
            {
                if (ID == aux.getID())
                {
                    return true;
                }
            }
            return false;
        }

        public static void insertar(int i, int c, float m, string nm, string di, float pe, int dd, int me, int aa, string T)
        {
            foreach(Cliente aux in ListaCliente)
            {
                if (aux.getID() == i)
                {
                    if (aux.chequear(c) == false)
                    {
                        Servicio nuevoCliente = new Servicio();
                        nuevoCliente.codigo = c;
                        nuevoCliente.monto = (int)m;
                        aux.agregar(nuevoCliente);
                        Mascotas nuevo;

                        if (T == "Perro")
                        {
                            nuevo = new Perro(nm, di, pe, c, new Fecha(dd, me, aa));
                        }
                        else
                        {
                            if(T == "Gato")
                            {
                                nuevo = new Gato(nm, di, pe, c, new Fecha(dd, me, aa));
                            }
                            else
                            {
                                nuevo = new Ave(nm, di, pe, c, new Fecha(dd, me, aa));
                            }
                        }
                        ListaMascota.Add(nuevo);
                    }
                }
            }
        }

        public static void insertar(string n, string a, int e, int i, int c, float m, string nm, string di, float pe, int dd, int me, int aa, string T)
        {
            Cliente C = new Cliente(n,a,e,i);
            C.agregar(new Servicio(c,m));
            ListaCliente.Add(C);
            guardar();

            Mascotas nuevo;

            if (T == "Perro")
            {
                nuevo = new Perro(nm, di, pe, c, new Fecha(dd, me, aa));
            }
            else
            {
                if (T == "Gato")
                {
                    nuevo = new Gato(nm, di, pe, c, new Fecha(dd, me, aa));
                }
                else
                {
                    nuevo = new Ave(nm, di, pe, c, new Fecha(dd, me, aa));
                }
            }
            ListaMascota.Add(nuevo);
        }

        public static void enviar(string n, string a, int e, int ID, int c, float m, string nm, string di, float pe, int dd, int me, int aa, string T)
        {
            if (verificar(ID) == true)
            {
                insertar(ID, c, m,nm,di,pe,dd,me,aa,T);
            }
            else
            {
                insertar(n,a,e,ID,c,m,nm, di, pe, dd, me, aa, T);
            }
        }
    }
}
