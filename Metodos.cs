
        [HttpGet("GetNombreCuentas")]
        public async Task<Respuesta<List<string>>> GetNombreCuentas()
        {
            LoggerManager logger = new LoggerManager();
            Respuesta<List<string>> oRespuesta = new Respuesta<List<string>>();
            List<string> cuentas = new List<string>();
            try
            {
                using (var context = _portalTramitesContext)
                {
                    var rta = context.cuentasGenericas
                        .OrderBy(t => t.creado)
                         .Select(t => t.nombreCGenerica)
                        .ToList();
                    if (rta.Count != 0)
                    {
                        cuentas = rta;
                        oRespuesta.Data = cuentas;
                        oRespuesta.Exito = 1;
                        oRespuesta.Mensaje = "Devolución lista de Cuentas Genericas exitosamente.";
                    }
                    else
                    {
                        oRespuesta.Data = cuentas;
                        oRespuesta.Exito = 1;
                        oRespuesta.Mensaje = "La lista de Cuentas Genericas no tiene información cargada.";
                    }
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Exito = 0;
                logger.LogError(ex.Message);
                oRespuesta.Mensaje = ex.Message;
            }
            return oRespuesta;
        }


    }
