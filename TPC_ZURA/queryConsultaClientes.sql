select cl.id as idcliente, us.id, pr.ID as idpersona, pr.Nombre, pr.Apellido, us.Usuario,
us.TipoUsuario as tipo, tius.nombre, pr.FNacimiento, pr.Email, pr.Telefono
from Clientes as cl inner join Usuarios as us
on cl.IDUsuario = us.ID
inner join Personas as pr
on us.IDPersona = pr.ID
inner join TiposUsuarios as tius
on us.TipoUsuario = tius.ID