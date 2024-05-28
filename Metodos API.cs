
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


     [HttpPost("Historico")]
        public async Task<Respuesta<negocioUmbral_Historico>> AddHistorico(negocio_Umbral_Historico model)
        {
            Respuesta<negocio_Umbral_Historico> oRespuesta = new Respuesta<negocio_Umbral_Historico>();
            LoggerManager logger = new LoggerManager();

            try
            {
                using (var context = _portalsContext)
                {
                    negocio_Umbral_Historico oUmbral = new negocio_Umbral_Historico();
                    oUmbral.umbral = model.umbral;
                    oUmbral.creado = model.creado;
                    oUmbral.creadoPor = model.creadoPor;
                    oUmbral.modificado = model.modificado;
                    oUmbral.modificadoPor = model.modificadoPor;
                    oUmbral.activo = model.activo;
                    oUmbral.fechaDesde = model.fechaDesde;
                    oUmbral.fechaHasta = model.fechaHasta;
                    oUmbral.modificadoPor_Historico = model.modificadoPor_Historico;
                    oUmbral.camposModificados = model.camposModificados;
                    oUmbral.idUmbral = model.idUmbral;

                    context.negocio_Umbral_Historico.Add(oUmbral);
                    context.SaveChanges();
                    oRespuesta.Data = oUmbral;
                    oRespuesta.Exito = 1;
                    oRespuesta.Mensaje = "El registro se ha agregado exitosamente";
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Exito = 0;
                logger.LogError("Error al insertar umbral", ex);
                oRespuesta.Mensaje = ex.Message;
            }
            return oRespuesta;
        }



   List<negocio _CC_Umbral> xxh = new List< negocio_Umbral>();

            try
            {
                var rta = _portalTramitesContext. negocio_Umbral
                       .Where(t => t.id == Id).ToListAsync();

                if (rta.Result.Count > 0)
                {
                    foreach ( negocio_Umbral xx in rta.Result)
                    {
                        xxh.Add(xx);
                    }
                    respuesta.Data = xxh;

                    respuesta.Exito = 1;
                    respuesta.Mensaje = "Devolución lista de xx exitosa.";
                }
                else
                {
                    respuesta.Exito = 1;
                    respuesta.Mensaje = $"No se encontró un xx con el Id: {Id}";
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


        [HttpPost("Historico")]
        public async Task<Respuesta< negocio_Umbral_Historico>> AddHistorico( negocio_Umbral_Historico model)
        {
            Respuesta< negocio_Umbral_Historico> oRespuesta = new Respuesta< negocio_Umbral_Historico>();
            LoggerManager logger = new LoggerManager();

            try
            {
                using (var context = _portalTramitesContext)
                {
                     negocio_Umbral_Historico oUmbral = new  negocio_Umbral_Historico();
                    oUmbral.xx = model.xx;
                    oUmbral.creado = model.creado;
                    oUmbral.creadoPor = model.creadoPor;
                    oUmbral.modificado = model.modificado;
                    oUmbral.modificadoPor = model.modificadoPor;
                    oUmbral.activo = model.activo;
                    oUmbral.fechaDesde = model.fechaDesde;
                    oUmbral.fechaHasta = model.fechaHasta;
                    oUmbral.modificadoPor_Historico = model.modificadoPor_Historico;
                   // oUmbral.camposModificados = model.camposModificados;


                    context. negocio_Umbral_Historico.Add(oUmbral);
                    context.SaveChanges();
                    oRespuesta.Data = oUmbral;
                    oRespuesta.Exito = 1;
                    oRespuesta.Mensaje = "El registro se ha agregado exitosamente";
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Exito = 0;
                logger.LogError("Error al insertar xx", ex);
                oRespuesta.Mensaje = ex.Message;
            }
            return oRespuesta;
        }

        
        [HttpGet("GetUmbralHistoricoById/{Id}")]
        public async Task<Respuesta<List< negocio_Umbral_Historico>>> GetUmbralHistoricoById(int Id)
        {
            Respuesta<List< negocio_Umbral_Historico>> respuesta = new Respuesta<List< negocio_Umbral_Historico>>();
            LoggerManager logger = new LoggerManager();

            try
            {
                var xxHistorico = await _portalTramitesContext. negocio_Umbral_Historico
                    .Where(t => t.id == Id)
                    .OrderByDescending(x => x.id)
                    .ToListAsync();

                if (xxHistorico.Count > 0)
                {
                    respuesta.Exito = 1;
                    respuesta.Data = xxHistorico; 
                    respuesta.Mensaje = "Devolución lista de xx exitosa.";
                }
                else
                {
                    respuesta.Exito = 0;
                    respuesta.Mensaje = $"No se encontró un xx con el Id: {Id}";
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



    }
    }

