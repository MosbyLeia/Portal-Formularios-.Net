
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


 [HttpGet("GetNombresAplicaciones")]
        public async Task<Respuesta<List<string>>> GetNombresAplicaciones()
        {
            LoggerManager logger = new LoggerManager();
            Respuesta<List<string>> respuesta = new Respuesta<List<string>>();
            List<string> listado = new List<string>();
            try
            {
                using (var context = _portalTramitesContext)
                {
                    var rta = context.listadoAplicaciones
                        .Where(t => t.activo == true)
                        .OrderBy(t => t.nombre)
                         .Select(t => t.nombre)
                        .ToList();
                    if (rta.Count != 0)
                    {
                        listado = rta;
                        respuesta.Data = listado;
                        respuesta.Exito = 1;
                        respuesta.Mensaje = "Devolución lista de listado de aplicacion exitosamente.";
                    }
                    else
                    {
                        respuesta.Data = listado;
                        respuesta.Exito = 0;
                        respuesta.Mensaje = "No existen listado de aplicacion activos.";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta.Exito = 0;
                logger.LogError(ex.Message);
                respuesta.Mensaje = ex.Message;
            }
            return respuesta;
        }



                            if (tramiteInstance.negocioSDR_SR_Aplicacion != null)
                            {
                                if (tramiteInstance.negocioSDR_SR_Aplicacion.Count > 0)
                                {
                                    foreach (var item in tramiteInstance.negocioSDR_SR_Aplicacion)
                                    {
                                        if (item != null)
                                        {
                                            var entitynegocioSDRSRAplicacion = await ConstructoresNegocios.CrearNegocioSDRSRAplicacionParaPost(entity.id, item);
                                            _portalTramitesContext.negocioSDR_SR_Aplicacion.Add(entitynegocioSDRSRAplicacion);
                                            int FInstanciaSDRSRAplicacion = await _portalTramitesContext.SaveChangesAsync();
                                        }
                                    }
                                }
                            }
                            if (tramiteInstance.negocioSDR_SR_CuentaGenerica != null)
                            {
                                if (tramiteInstance.negocioSDR_SR_CuentaGenerica.Count > 0)
                                {
                                    foreach (var item in tramiteInstance.negocioSDR_SR_CuentaGenerica)
                                    {
                                        if (item != null)
                                        {
                                            var entitynegocioSDRSRCuentaGenerica = await ConstructoresNegocios.CrearNegocioSDRSRCuentaGenericaParaPost(entity.id, item);
                                            _portalTramitesContext.negocioSDR_SR_CuentaGenerica.Add(entitynegocioSDRSRCuentaGenerica);
                                            int FInstanciaSDRSRCuentaGenerica = await _portalTramitesContext.SaveChangesAsync();
                                        }
                                    }
                                }
                            }

