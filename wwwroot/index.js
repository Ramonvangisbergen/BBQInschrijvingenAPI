$(document).ready(() => 
{
	this.setOnFormuleAantalChange();
	this.setOnDessertAantalChange();
	this.setOnSnackAantalChange();
	$('form').submit(this.onSubmit);
});

setOnFormuleAantalChange = () => 
{
	$(".formule-aantal").each((i, formuleInput) => 
	{		
		formuleInput.addEventListener("change", (event) => 
		{			
			let totalElementId = "#totaal-formule-" + event.target.id.split('-')[2];
			$(totalElementId).val(event.target.dataset.price * event.target.value);
			
			this.calculateTotalPrice();
			
			$(`[data-parent="${event.target.id}"]`).each((i, snackInput) =>
			{
				let isDisabled = event.target.value == 0;
				snackInput.disabled = isDisabled;

				if(isDisabled)
				{
					snackInput.value = 0;
					$(`#${event.target.id}-message`).html("");
				}
				else 
				{
					
					let totaalformules = 0;
	
					$(`[data-parent="${event.target.id}"]`).each((i, inputElement) => 
					{
						totaalformules += Number(inputElement.value);		
					});
							
					let maxSnacks = $(`#${event.target.id}`).val() * $(`#${event.target.id}`).data("snacks");
					let msg = "";
					if(maxSnacks > totaalformules) 
					{
						msg = `U kunt nog ${maxSnacks - totaalformules} snack(s) kiezen`;
					}
					else if (maxSnacks < totaalformules) 
					{
						msg = `U heeft ${totaalformules - maxSnacks} snacks te veel gekozen`;
					}
					
					$(`#${event.target.id}-message`).html(`<p>${msg}</p>`);
				}
			});		
		});
	});
}

setOnDessertAantalChange = () => 
{
	$(".snack").each((i, dessertInput) => 
		{		
			dessertInput.addEventListener("change", (event) => 
			{			
				$(`#totaal-${event.target.id}`).val(event.target.value);			
				this.calculateTotalPrice();
				$("#totaal-desserten").val(this.calculateDessertTotal());
			});
		});
}

setOnSnackAantalChange = () => {
	$(".snack").each((i, snackInput) => {
		snackInput.addEventListener("change", (event) => {
			let totaal = 0;
			let parent = event.target.dataset.parent;

			$(`[data-parent="${parent}"]`).each((i, sibling) => {
				totaal += Number(sibling.value);
			});

			let maxSnacks = $(`#${parent}`).val() * $(`#${parent}`).data("snacks");
			let msg = "";
			if (maxSnacks > totaal) {
				msg = "U kunt nog " + (maxSnacks - totaal) + " snack(s) kiezen";
			}
			else if (maxSnacks < totaal) {
				msg = "U heeft " + (totaal - maxSnacks) + " snacks te veel gekozen";
			}

			$(`#${parent}-message`).html(`<p>${msg}</p>`);
		});
	});
}

calculateTotalPrice = () => 
{
	let totaal = 0;
	$(".formule-totaal").each((i, formuleTotaal) => 
	{
		totaal+= Number(formuleTotaal.value);
	});
	
	totaal += this.calculateDessertTotal();
	
	$("#totaal-bbq").val(totaal);
}

calculateDessertTotal = () => 
{
	let totaal = 0;
	$(".dessert-totaal").each((i, dessertTotaal) => 
	{
		totaal+= Number(dessertTotaal.value);
	});

	return totaal;
}

onSubmit = (event) => 
{
		
	event.preventDefault();
	
	let messages = [];
	let aantalPersonen = Number($("#aantal-personen").val());

	let totaalAantalGekozenFormules = 0;
	$(".formule-aantal").each((i, formule) => 
	{
		totaalAantalGekozenFormules += Number(formule.value);
	});
	
	if(totaalAantalGekozenFormules != aantalPersonen)
	{
		messages.push(`-U heeft aangeduid dat u voor ${aantalPersonen} personen wilt inschrijven. Maar U heeft ${totaalAantalGekozenFormules} formule(s) aangeduid.`);
	}
	
	
	for(let i = 1; i <= 4; i++)
	{
		
		let aantalGekozenSnacks = 0;
		$(`[data-parent="aantal-formule-${i}`).each((j, snack) => 
		{
			aantalGekozenSnacks += Number(snack.value);
		});

		let maxSnacksVoorFormule = Number($("#aantal-formule-" + i).data("snacks")) * Number($("#aantal-formule-" + i).val());
		if(aantalGekozenSnacks != maxSnacksVoorFormule)
		{
			messages.push(`-U heeft ${aantalGekozenSnacks > maxSnacksVoorFormule ? "te veel" : "te weinig"} snacks gekozen voor Formule ${i}`);
		}
	} 
	
	if (messages.length > 0)
	{
		messages.unshift("Inschrijving mislukt om de volgende reden(en):");
		$("#form-msg").html(`<p class="error">${messages.join("</p><p>")}</p>`);
	}
	else 
	{
		$("#frmBbq :input").attr("disabled", true);

		//post
		$("#form-msg").html(`<p>Bezig met het verwerken van uw inschrijving...</p>`);
		let inschrijving = {
			'naam': $("#naam").val().toUpperCase(),
			'naamInterne': $("#naam-interne").val().toUpperCase(),
			'aantalPersonen': $("#aantal-personen").val(),
			'reservatie': $("input[name=reservatie]:checked").val(),
			'formule1': {
				'aantal': $("#aantal-formule-1").val(),
				'hamburgers': $("#formule-1-hamburger").val(),
				'kipfilets': $("#formule-1-kipfilet").val(),
				'worsten': $("#formule-1-worst").val(),
				'sates': $("#formule-1-sate").val(),
			},
			'formule2': {
				'aantal': $("#aantal-formule-2").val(),
				'hamburgers': $("#formule-2-hamburger").val(),
				'kipfilets': $("#formule-2-kipfilet").val(),
				'worsten': $("#formule-2-worst").val(),
				'sates': $("#formule-2-sate").val(),
			},
			'formule3': {
				'aantal': $("#aantal-formule-3").val(),
				'hamburgers': $("#formule-3-hamburger").val(),
				'worsten': $("#formule-3-worst").val(),
				'balletjes': $("#formule-3-balletjes").val(),
			},
			'formule4': {
				'aantal': $("#aantal-formule-4").val(),
				'hamburgers': $("#formule-4-hamburger").val(),
				'worsten': $("#formule-4-worst").val(),
				'balletjes': $("#formule-4-balletjes").val(),
			},
			'desserten': {
				'aantal': $("#totaal-desserten").val(),
				'chocomousses': $("#chocomousse").val(),
				'rijstpappen': $("#rijstpap").val(),
				'dameBlanches': $("#dame-blanche").val(),
			}
		}
		$.ajax(
			{ 
				url: '/BBQInschrijvingen/submit',
				type: 'POST',
				contentType: 'application/json', 
				timeout: 150000, // adjust the limit. currently its 1500 seconds
				data: JSON.stringify(inschrijving),
				success: (response) =>
				{
					if (response.success)
					{
						$("#form-msg").html(
							`<p class="succes">We hebben uw inschrijving correct ontvangen!</p>` +
							`<p>U kan het bedrag storten op rek.nr. <b>BE18 7330 3207 1765</b> t.a.v. Instituut van het Heilig-Graf met vermelding van: <b>BBQ internaat + naam interne</b> (indien geen link met de interne gewoon de eigen naam). </p>` +
							`<p><b>Stortingen dienen te gebeuren voor 23 mei</b>. Gepast betalen kan ook, voordien af te geven bij de opvoeders of bij inkom. (ook payconinc aan inkom)</p>` +
							`<p>Ingang Baron Du Fourstraat poort D, mogelijkheid tot parkeren poort E.</p>` +
							`<br/>` +
							`<p>Binnen enkele seconden wordt automatisch een uitreksel van uw inschrijving gedownload. Op dit ticket vind u uw bestelling terug. Indien dit niet gebeurt kunt u uw uitreksel downloaden via onderstaande knop:</p>` +

							`<p><button class="btn btn-primary" click="downloadTicket(${$('#inschrijving').val(JSON.stringify(inschrijving))})">Download Uitreksel</button></p>`
						);

						$('#inschrijving').val(JSON.stringify(response.data));

						//download ticket
						this.downloadTicket(inschrijving);
					}
					else {
						$("#form-msg").html(
							`<p class="error">Er is iets fout gelopen bij het verwerken van uw opschrijving. Probeer het opnieuw. Als het dan nog niet lukt, neem screenshots van het volledige inschrijvingsformulier inculsief deze foutmelding en stuur deze door via mail naar <a href="mailto:ramon.vangisbergen@heilig-graf.be.com?subject=Foutmelding Inschrijvingsformulier BBQ Internaat">ramon.vangisbergen@heilig-graf.be.</a> </p>` +
							`<p class="error">Message: ${response.data.message}</p>`
						);
					}
				}
				, error: (jqXHR, textStatus, err) => {
					$("#form-msg").html(
						`<p class="error">Er is iets fout gelopen bij het verwerken van uw opschrijving. Probeer het opnieuw. Als het dan nog niet lukt, neem screenshots van het volledige inschrijvingsformulier inculsief deze foutmelding en stuur deze door via mail naar <a href="mailto:ramon.vangisbergen@heilig-graf.be.com?subject=Foutmelding Inschrijvingsformulier BBQ Internaat">ramon.vangisbergen@heilig-graf.be.</a> </p>` + 
						`<p class="error">Error: ${err}</p>`
					);
				}
		 	}
		);   

		
	}
		
	return false;	
}

downloadTicket = (inschrijving) => {
	let xhrOverride = new XMLHttpRequest();
	xhrOverride.responseType = 'arraybuffer';
	$.ajax({
		url: '/BBQInschrijvingen/GenerateTicketPDF/',
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
			document.body.removeChild(a)

		}
	});
}


