# Eureka

public async Task<IActionResult> ObtenerListarPorArea(int? id, int page)
        {
            var query = _context.Perfiles.Include(p=>p.Estado).Include(p => p.Rol).Where(p => p.AreaId > 0);
            if(id.HasValue)
                query = query.Where(p => p.AreaId == id.Value);

            var total = query.Count();
            query = query.Skip((page - 1) * 10).Take(10);

            return Json(new
            {
                total,
                data = await query.Select(x =>
                new
                {
                    x.Username,
                    x.Nombre,
                    x.Correo,
                    x.Telefono,
                    x.Rol.Descripcion,
                    Estado = x.Estado.Descripcion
                }).ToListAsync()
            }, config);
        }


	<script>       
        $(document).ready(function () {
            
            let perfilModel = findEntity('perfil');
            perfilModel.load();

        });

        var load = (entity,page) => {
            let model = findEntity(entity);
            model.load(page);
        }

    </script>

	<div class="col-md-12">
    <div class="tab-base">
        <input type="hidden" asp-for="Id" />
        <!--Nav Tabs-->
        <ul class="nav nav-tabs">
            <li class="active">
                <a data-toggle="tab" href="#tabusuarios" aria-expanded="false"><span class="fa fa-users"></span> Usuarios <span class="badge badge-success">0</span></a>
            </li>
            <li class="">
                <a data-toggle="tab" href="#tabcompras" aria-expanded="false"><span class="ti-shopping-cart"></span> Compras <span class="badge badge-primary">0</span></a>
            </li>
            <li class="">
                <a data-toggle="tab" href="#tabentradas" aria-expanded="false"><span class="fa fa-sign-in"></span> Entradas <span class="badge badge-info">0</span></a>
            </li>
            <li class="">
                <a data-toggle="tab" href="#tabsalidas" aria-expanded="false"><span class="fa fa-sign-out"></span> Salidas <span class="badge badge-purple">0</span></a>
            </li>
        </ul>

        <!--Tabs Content-->
        <div class="tab-content">
            <div id="tabusuarios" class="tab-pane fade active in">
                <div id="perfiles"></div>
            </div>
            <div id="tabcompras" class="tab-pane fade">
                <div id="compras"></div>
            </div>
            <div id="tabentradas" class="tab-pane fade">
                <div id="entradas"></div>
            </div>
            <div id="tabsalidas" class="tab-pane fade">
                <div id="salidas"></div>
            </div>
        </div>
    </div>

</div>