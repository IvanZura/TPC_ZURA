select emp.id as idempleado, us.id, pr.ID as idpersona, pr.Nombre, pr.Apellido, us.Usuario,
us.TipoUsuario as tipo, tius.nombre, pr.FNacimiento, pr.Email, pr.Telefono
from Empleados as emp inner join Usuarios as us
on emp.IDUsuario = us.ID
inner join Personas as pr
on us.IDPersona = pr.ID
inner join TiposUsuarios as tius
on us.TipoUsuario = tius.ID