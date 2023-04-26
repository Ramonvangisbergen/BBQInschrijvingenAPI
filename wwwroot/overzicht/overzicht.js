$(document).ready(() =>
{
	$("#container-overzicht").hide();

	$("#btnValidatePassword").click(this.validatePassword);
	$('#btnSaveDbToJSON').click(this.saveDbAsJSON);
});

getInschrijvingen = () =>
{
	let totaalHamburgers = 0;
	let totaalKipfilets = 0;
	let totaalWorsten = 0;
	let totaalSates = 0;
	let totaalVeggieBurger = 0;
	let totaalVeggieWorsten = 0;
	let totaalVeggieBalletjes = 0;
	let totaalAantalPersonen = 0;
	let totaalChocomousses = 0;
	let totaalRijstpappen = 0;
	let totaalDameBlanches = 0;
	let aantalBetaald = 0;
	let aantalAanwezig = 0;

	$.get("../BBQInschrijvingen/GetAllInschrijvingen", (response) => {
		if (response.success && response.data) {
			var inschrijvingen = response.data;

			for (i = 0; i < inschrijvingen.length; i++) {
				var aantalHamburgers = (inschrijvingen[i].formule1.hamburgers != null ? inschrijvingen[i].formule1.hamburgers : 0) +
					(inschrijvingen[i].formule2.hamburgers != null ? inschrijvingen[i].formule2.hamburgers : 0);
				
				totaalHamburgers += aantalHamburgers

				var aantalKipfilets = (inschrijvingen[i].formule1.kipfilets != null ? inschrijvingen[i].formule1.kipfilets : 0) +
					(inschrijvingen[i].formule2.kipfilets != null ? inschrijvingen[i].formule2.kipfilets : 0);

				totaalKipfilets += aantalKipfilets;

				var aantalWorsten = (inschrijvingen[i].formule1.worsten != null ? inschrijvingen[i].formule1.worsten : 0) +
					(inschrijvingen[i].formule2.worsten != null ? inschrijvingen[i].formule2.worsten : 0);
				
				totaalWorsten += aantalWorsten;

				var aantalSates = (inschrijvingen[i].formule1.sates != null ? inschrijvingen[i].formule1.sates : 0) +
					(inschrijvingen[i].formule2.sates != null ? inschrijvingen[i].formule2.sates : 0);

				totaalSates += aantalSates;

				var aantalVeggieBurgers = (inschrijvingen[i].formule3.hamburgers != null ? inschrijvingen[i].formule3.hamburgers : 0) +
					(inschrijvingen[i].formule4.hamburgers != null ? inschrijvingen[i].formule4.hamburgers : 0);

				totaalVeggieBurger += aantalVeggieBurgers;

				var aantalVeggieWorsten = (inschrijvingen[i].formule3.worsten != null ? inschrijvingen[i].formule3.worsten : 0) +
					(inschrijvingen[i].formule4.worsten != null ? inschrijvingen[i].formule4.worsten : 0);

				totaalVeggieWorsten += aantalVeggieWorsten;

				var aantalVeggieBalletjes = (inschrijvingen[i].formule3.balletjes != null ? inschrijvingen[i].formule3.balletjes : 0) +
					(inschrijvingen[i].formule4.balletjes != null ? inschrijvingen[i].formule4.balletjes : 0);

				totaalVeggieBalletjes += aantalVeggieBalletjes;
				totaalAantalPersonen += inschrijvingen[i].aantalPersonen;

				totaalChocomousses += inschrijvingen[i].desserten.chocomousses ? inschrijvingen[i].desserten.chocomousses : 0;
				totaalRijstpappen += inschrijvingen[i].desserten.rijstpappen ? inschrijvingen[i].desserten.rijstpappen : 0;
				totaalDameBlanches += inschrijvingen[i].desserten.dameBlanches ? inschrijvingen[i].desserten.dameBlanches : 0;

				aantalBetaald += inschrijvingen[i].isBetaald ? 1 : 0;
				aantalAanwezig += inschrijvingen[i].isAanwezig ? 1 : 0;

				$('#tbl-overzicht tbody').append(
					`<tr>
						<td>${inschrijvingen[i].id}</td>
						<td>
							<input id="inschrijving-${i}" type="hidden"/>
							<button id="inschrijving-${i}-download" class="btn btn-primary">ticket</button>
						</td>
						<td>
							<button id="inschrijving-${i}-delete" class="btn btn-danger">Verwijder</button>
						</td>
						<td><input id="inschrijving-${i}-isBetaald" type="checkbox"/></td>
						<td><input id="inschrijving-${i}-isAanwezig" type="checkbox"/></td>
						<td>${inschrijvingen[i].naam}</td>
						<td>${inschrijvingen[i].naamInterne}</td>
						<td>${inschrijvingen[i].aantalPersonen}</td>
						<td>${inschrijvingen[i].reservatie}</td>
						<td>&#8364;${inschrijvingen[i].totaalBedrag}</td>
						<td>${inschrijvingen[i].formule1.aantal ? inschrijvingen[i].formule1.aantal : 0}</td>
						<td>${inschrijvingen[i].formule2.aantal ? inschrijvingen[i].formule2.aantal : 0}</td>
						<td>${inschrijvingen[i].formule3.aantal ? inschrijvingen[i].formule3.aantal : 0}</td>
						<td>${inschrijvingen[i].formule4.aantal ? inschrijvingen[i].formule4.aantal : 0}</td>
						<td>${aantalHamburgers}</td>
						<td>${aantalKipfilets}</td>
						<td>${aantalWorsten}</td>
						<td>${aantalSates}</td>
						<td>${aantalVeggieBurgers}</td>
						<td>${aantalVeggieWorsten}</td>
						<td>${aantalVeggieBalletjes}</td>
						<td>${inschrijvingen[i].desserten.chocomousses ? inschrijvingen[i].desserten.chocomousses : 0}</td>
						<td>${inschrijvingen[i].desserten.rijstpappen ? inschrijvingen[i].desserten.rijstpappen : 0}</td>
						<td>${inschrijvingen[i].desserten.dameBlanches ? inschrijvingen[i].desserten.dameBlanches : 0}</td>
					</tr>`	
				);

				$("#inschrijving-" + i).val(JSON.stringify(inschrijvingen[i]));

				$(`#inschrijving-${i}-isBetaald`).prop('checked', inschrijvingen[i].isBetaald);
				$(`#inschrijving-${i}-isAanwezig`).prop('checked', inschrijvingen[i].isAanwezig);

				$("#inschrijving-" + i).val(JSON.stringify(inschrijvingen[i]));

				let rowId = i;
				$(`#inschrijving-${i}-download`).click(() => { this.downloadTicketByRowId(rowId); });
				$(`#inschrijving-${i}-delete`).click(() => { this.deleteInschrijving(rowId); });
				$(`#inschrijving-${i}-isBetaald`).change(() => { this.updateInschrijving(rowId); });
				$(`#inschrijving-${i}-isAanwezig`).change(() => { this.updateInschrijving(rowId); });
			}

			$('#totaalHamburgers').val(totaalHamburgers);
			$('#totaalKipfilets').val(totaalKipfilets);
			$('#totaalWorsten').val(totaalWorsten);
			$('#totaalSates').val(totaalSates);
			$('#totaalVeggieBurger').val(totaalVeggieBurger);
			$('#totaalVeggieWorsten').val(totaalVeggieWorsten);
			$('#totaalVeggieBalletjes').val(totaalVeggieBalletjes);
			$('#totaalAantalPersonen').val(totaalAantalPersonen);
			$('#totaalChocomousses').val(totaalChocomousses);
			$('#totaalRijstpappen').val(totaalRijstpappen);
			$('#totaalDameBlanches').val(totaalDameBlanches);
			$('#aantalBetaald').val(`${aantalBetaald}/${inschrijvingen.length}`);
			$('#aantalAanwezig').val(`${aantalAanwezig}/${inschrijvingen.length}`);
			$('#totaalInschrijvingen').val(inschrijvingen.length);

			$('#inschrijvingen').val(JSON.stringify(inschrijvingen));
		}
		else
		{
			alert('Iets is misgegaan bij het ophalen van de inschrijvingen');
		}

		$('#tbl-overzicht').DataTable({ paging: false });
	});
}

downloadTicketByRowId = (id) => {
	let inschrijving = JSON.parse($("#inschrijving-" + id).val());
	this.downloadTicket(inschrijving);
}

downloadTicket = (inschrijving) => {
	let xhrOverride = new XMLHttpRequest();
	xhrOverride.responseType = 'arraybuffer';

	$.ajax({
		url: '../BBQInschrijvingen/GenerateTicketPDF/',
		method: 'POST',
		contentType: 'application/json',
		data: JSON.stringify(inschrijving),
		xhr: () => { return xhrOverride }
	}).done((data, status, xhr) => {
		if (status == "success") {

			let byteArray = new Uint8Array(data);
			let a = window.document.createElement('a');

			a.href = window.URL.createObjectURL(new Blob([byteArray], { type: 'application/octet-stream' }));
			a.download = `Inschrijving BBQ Heilig Graf_${inschrijving.naam}.pdf`;

			// Append anchor to body.
			document.body.appendChild(a)
			a.click();

			// Remove anchor from body
			document.body.removeChild(a);
		}
	});
}

deleteInschrijving = (rowId) => {
	if (confirm("Weet u zeker dat u deze inschrijving wilt verwijderen? Een verwijdering is permanent!")) {
		let inschrijving = JSON.parse($("#inschrijving-" + rowId).val());
		let data = { 'password': $("#password").val(), 'model': inschrijving.id };

		$.ajax({
			url: '../BBQInschrijvingen/DeleteInschrijving',
			method: 'POST',
			data: JSON.stringify(data),
			contentType: 'application/json',
			success: (response) => {
				if (response.success) {

					$('#tbl-overzicht').DataTable().clear().destroy();
					this.getInschrijvingen();
				} else {
					alert('Verwijderen mislukt: ' + response.message);
				}
			}
		});
	}
}

updateInschrijving = (rowId) => {
	let inschrijving = JSON.parse($("#inschrijving-" + rowId).val());
	inschrijving.isBetaald = $(`#inschrijving-${rowId}-isBetaald`).is(':checked');
	inschrijving.isAanwezig = $(`#inschrijving-${rowId}-isAanwezig`).is(':checked');

	let data = { 'password': $("#password").val(), 'model': inschrijving };

	$.ajax({
		url: '../BBQInschrijvingen/UpdateInschrijving',
		method: 'POST',
		contentType: 'application/json',
		data: JSON.stringify(data),
		success: (response) => {
			if (response.success) {
				$('#tbl-overzicht').DataTable().clear().destroy();
				this.getInschrijvingen();
			} else {
				alert('Aanpassen inschrijving mislukt: ' + response.message);
			}
		}
	});
}

saveDbAsJSON = () => {
	let data = $('#inschrijvingen').val();
	let filename = 'bbq_inschrijvingen_db.json';
	let type = "application/json";
	let file = new Blob([data], { type: type });
	if (window.navigator.msSaveOrOpenBlob) // IE10+
		window.navigator.msSaveOrOpenBlob(file, filename);
	else { // Others
		let a = document.createElement("a"),
			url = URL.createObjectURL(file);
		a.href = url;
		a.download = filename;
		document.body.appendChild(a);
		a.click();
		setTimeout(() => {
			document.body.removeChild(a);
			window.URL.revokeObjectURL(url);
		}, 0);
	}
}

validatePassword = () => {
	let password = $("#password").val();
	$.ajax({
		url: "../BBQInschrijvingen/ValidatePassword/" + password,
		method: 'GET',
		contentType: 'application/json',
		success: (response) => {
			if (response.success && response.isValidated) {
				$("#container-overzicht").show();
				$("#container-password").hide();

				this.getInschrijvingen();
			} else {
				$("#msg").html("<p>Verkeerd wachtwoord</p>");
			}
		}
	});
}

